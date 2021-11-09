using UnityEngine;

public class Tilemap2D : MonoBehaviour
{
    [Header("Tile")]
    [SerializeField]
    private GameObject tilePrefab;

    [Header("Item")]
    [SerializeField]
    private GameObject itemPrefab;

    public void GenerateTilemap(MapData mapData)
    {
        int width = mapData.mapSize.x;
        int height = mapData.mapSize.y;

        for (int y = 0; y < height; ++ y)
        {
            for (int x = 0; x < width; ++ x)
            {
                // ���� ���·� ��ġ�� Ÿ�ϵ��� ���� ��ܺ��� ���������� ��ȣ�� �ο�
                // 0, 1, 2, 3, 4, 5
                // 6, 7, ...
                int index = y * width + x;

                // Ÿ���� �Ӽ��� "Empty"�̸� �ƹ��͵� �������� �ʰ� ����д�.
                if(mapData.mapData[index] == (int)TileType.Empty)
                {
                    continue;
                }

                // �����Ǵ� Ÿ�ϸ��� �߾���(0,0,0)�� ��ġ
                Vector3 position = new Vector3(-(width * 0.5f - 0.5f) + x, (height * 0.5f - 0.5f) - y);

                // ���� index�� �� ������ TileType.Empty(0)���� ũ��, TileType.LastIndex(8)���� ������
                if ( mapData.mapData[index] > (int)TileType.Empty && mapData.mapData[index] < (int)TileType.LastIndex)
                {
                    // Ÿ�� ����
                    SpawnTile((TileType)mapData.mapData[index], position);
                }
                // ���� index�� �� ������ ItemType.Coin(10) �̸�
                else if (mapData.mapData[index] == (int)ItemType.Coin)
                {
                    // ������ ����
                    SpawnItem(position);
                }
            }
        }
    }

    private void SpawnTile(TileType tileType, Vector3 position)
    {
        GameObject clone = Instantiate(tilePrefab, position, Quaternion.identity);

        clone.name = "Tile"; // Tile ������Ʈ�� �̸��� "Tile"�� ����
        clone.transform.SetParent(transform); // Tilemap2D ������Ʈ�� Tile ������Ʈ�� �θ�� ����

        Tile tile = clone.GetComponent<Tile>(); // ��� ������ Ÿ��(clone) ������Ʈ�� Tile.Setup() �޼ҵ� ȣ��
        tile.Setup(tileType);
    }

    private void SpawnItem(Vector3 position)
    {
        GameObject clone = Instantiate(itemPrefab, position, Quaternion.identity);

        clone.name = "Item"; // Item ������Ʈ�� �̸��� "Item"���� ����
        clone.transform.SetParent(transform); // Tilemap2D ������Ʈ�� Item ������Ʈ�� �θ�� ����
    }
}
