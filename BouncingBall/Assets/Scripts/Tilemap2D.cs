using UnityEngine;

public class Tilemap2D : MonoBehaviour
{
    [Header("Common")]
    [SerializeField]
    private StageController stageController;
    [SerializeField]
    private StageUI stageUI;

    [Header("Tile")]
    [SerializeField]
    private GameObject tilePrefab;

    [Header("Item")]
    [SerializeField]
    private GameObject itemPrefab;

    private int maxCoinCount = 0; // 현재 스테이지에 존재하는 최대 코인 개수
    private int currentCoinCount = 0; // 현재 스테이지에 존재하는 현재 코인 개수

    public void GenerateTilemap(MapData mapData)
    {
        int width = mapData.mapSize.x;
        int height = mapData.mapSize.y;

        for (int y = 0; y < height; ++ y)
        {
            for (int x = 0; x < width; ++ x)
            {
                // 격자 형태로 배치된 타일들을 왼쪽 상단부터 순차적으로 번호를 부여
                // 0, 1, 2, 3, 4, 5
                // 6, 7, ...
                int index = y * width + x;

                // 타일의 속성이 "Empty"이면 아무것도 생성하지 않고 비워둔다.
                if(mapData.mapData[index] == (int)TileType.Empty)
                {
                    continue;
                }

                // 생성되는 타일맵의 중앙이(0,0,0)인 위치
                Vector3 position = new Vector3(-(width * 0.5f - 0.5f) + x, (height * 0.5f - 0.5f) - y);

                // 현재 index의 맵 정보가 TileType.Empty(0)보다 크고, TileType.LastIndex(8)보다 작으면
                if ( mapData.mapData[index] > (int)TileType.Empty && mapData.mapData[index] < (int)TileType.LastIndex)
                {
                    // 타일 생성
                    SpawnTile((TileType)mapData.mapData[index], position);
                }
                // 현재 index의 맵 정보가 ItemType.Coin(10) 이면
                else if (mapData.mapData[index] == (int)ItemType.Coin)
                {
                    // 아이템 생성
                    SpawnItem(position);
                }
            }
        }

        currentCoinCount = maxCoinCount;
        // 현재 코인의 개수가 바뀔 때마다 UI 출력 정보 갱신
        stageUI.UpdateCoinCount(currentCoinCount, maxCoinCount);
    }

    private void SpawnTile(TileType tileType, Vector3 position)
    {
        GameObject clone = Instantiate(tilePrefab, position, Quaternion.identity);

        clone.name = "Tile"; // Tile 오브젝트의 이름을 "Tile"로 설정
        clone.transform.SetParent(transform); // Tilemap2D 오브젝트를 Tile 오브젝트의 부모로 설정

        Tile tile = clone.GetComponent<Tile>(); // 방금 생성한 타일(clone) 오브젝트의 Tile.Setup() 메소드 호출
        tile.Setup(tileType);
    }

    private void SpawnItem(Vector3 position)
    {
        GameObject clone = Instantiate(itemPrefab, position, Quaternion.identity);

        clone.name = "Item"; // Item 오브젝트의 이름을 "Item"으로 설정
        clone.transform.SetParent(transform); // Tilemap2D 오브젝트를 Item 오브젝트의 부모로 설정

        // 현재 아이템은 코인 밖에 없기 때문에 생성한 아이템의개수 = 코인 개수
        maxCoinCount++;
    }

    public void GetCoin(GameObject coin)
    {
        currentCoinCount--; // 현재 코인 개수 -1
        // 현재 코인의 개수가 바뀔때 마다 UI 출력 정보 갱신
        stageUI.UpdateCoinCount(currentCoinCount, maxCoinCount);

        // 코인 아이템이 사라질 때 호출하는 Item.Exit() 메소드 호출
        coin.GetComponent<Item>().Exit();

        // 현재 스테이지에 코인 개수가 0 이면
        if ( currentCoinCount == 0)
        {
            // 게임 클리어
            stageController.GameClear();
        }
    }
}
