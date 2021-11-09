using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tilemap2D : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab; // �ʿ� ��ġ�Ǵ� Ÿ�� ������

    [SerializeField]
    private TMP_InputField inputWidth; // ���� width ũ�⸦ ������ InputField
    [SerializeField]
    private TMP_InputField inputHeight; // ���� height ũ�⸦ ������ InputField

    private MapData mapData; // �� ������ ���忡 ���Ǵ� ������ ��� Ŭ����

    // �� x, y ũ�� ������Ƽ
    public int Width { private set; get; } = 10;
    public int Height { private set; get; } = 10;
        
    // �ʿ� ��ġ�Ǵ� Ÿ�� ���� ������ ���� List ������Ƽ
    public List<Tile> TileList { private set; get; }
    private void Awake()
    {
        // InputField�� ǥ�õǴ� �⺻ �� ����
        inputWidth.text = Width.ToString();
        inputHeight.text = Height.ToString();

        //GenerateTilemap();
        mapData = new MapData();
        TileList = new List<Tile>();
    }

    public void GenerateTilemap()
    {
        // out �Ǵ� ref Ű���带 ����� �� ������Ƽ ����� �Ұ����ϱ� ������ �������� ����
        int width, height;

        // InputField�� �ִ� width, height ���ڿ��� width, height ������ ������ ����
        int.TryParse(inputWidth.text, out width);
        int.TryParse(inputHeight.text, out height);

        // ������Ƽ Width, Height �� ����
        Width = width;
        Height = height;

        for (int y =0; y< Height; ++y)
        {
            for(int x = 0; x < Width; ++ x)
            {
                // �����Ǵ� Ÿ�ϸ��� �߾��� (0, 0, 0)�� ��ġ
                Vector3 position = new Vector3((-Width * 0.5f + 0.5f) + x, (Height * 0.5f - 0.5f) - y, 0);

                SpawnTile(TileType.Empty, position);
            }
        }

        mapData.mapSize.x = Width;
        mapData.mapSize.y = Height;
        mapData.mapData = new int[TileList.Count];
    }

    private void SpawnTile(TileType tileType, Vector3 position)
    {
        GameObject clone = Instantiate(tilePrefab, position, Quaternion.identity);

        clone.name = "Tile"; // Tile ������Ʈ�� �̸��� "Tile"�� ����
        clone.transform.SetParent(transform); // Tilemap2D ������Ʈ�� Tile ������Ʈ�� �θ�� ����

        Tile tile = clone.GetComponent<Tile>(); // ��� ������ Ÿ��(clone) ������Ʈ�� Tile.Setup() �޼ҵ� ȣ��
        tile.Setup(tileType);

        TileList.Add(tile); // ��� Ÿ�� ������ tileList ����Ʈ�� ����
    }

    public MapData GetMapData()
    {
        // �ʿ� ��ġ�� ��� Ÿ���� ������ mapData.mapData �迭�� ����
        for (int i = 0; i< TileList.Count; ++i)
        {
            if (TileList[i].TileType != TileType.Player)
            {
                mapData.mapData[i] = (int)TileList[i].TileType;
            }
            // ���� ��ġ�� Ÿ���� �÷��̾��̸�
            else
            {
                // ���� ��ġ�� Ÿ���� �� Ÿ��(Empty)�� ����
                mapData.mapData[i] = (int)TileType.Empty;

                // ���� ��ġ ������ mapData.playerPosition�� ����
                int x = (int)TileList[i].transform.position.x;
                int y = (int)TileList[i].transform.position.y;
                mapData.playerPosition = new Vector2Int(x, y);
            }
        }

        return mapData;
    }
}
