using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField]
    private Tilemap2D tilemap2D; // MapData ������ �������� ���� �����ϱ� ���� Tilemap2D
    [SerializeField]
    private PlayerController playerController; // �÷��̾� Setup() �޼ҵ� ȣ���� ���� PlayerController

    private void Awake()
    {
        // MapDataLoader Ŭ���� �ν��Ͻ� ���� �� �޸� �Ҵ�
        MapDataLoader mapDataLoader = new MapDataLoader();

        // ���� ����Ǿ� �ִ� json ������ Stage01, Stage02.. �̱� ������ "Stage01" �����͸� �ҷ��´�
        MapData mapData = mapDataLoader.Load("Stage01");

        // mapData ������ �������� Ÿ�� ������ �� ����
        tilemap2D.GenerateTilemap(mapData);

        // mapData.playerPosition ������ �������� �÷��̾� ��ġ ����
        playerController.Setup(mapData.playerPosition);
    }
}
