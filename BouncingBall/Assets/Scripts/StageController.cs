using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField]
    private Tilemap2D tilemap2D; // MapData ������ �������� ���� �����ϱ� ���� Tilemap2D
    [SerializeField]
    private PlayerController playerController; // �÷��̾� Setup() �޼ҵ� ȣ���� ���� PlayerController
    [SerializeField]
    private CameraController cameraController; // ī�޶� �þ� ������ ���� CameraController
    [SerializeField]
    private StageUI stageUI;

    private void Awake()
    {
        // MapDataLoader Ŭ���� �ν��Ͻ� ���� �� �޸� �Ҵ�
        MapDataLoader mapDataLoader = new MapDataLoader();

        // ���� ����Ǿ� �ִ� json ������ Stage01, Stage02.. �̱� ������ "Stage01" �����͸� �ҷ��´�
        MapData mapData = mapDataLoader.Load("Stage01");

        // mapData ������ �������� Ÿ�� ������ �� ����
        tilemap2D.GenerateTilemap(mapData);

        // mapData.playerPosition ������ �������� �÷��̾� ��ġ ����
        playerController.Setup(mapData.playerPosition, mapData.mapSize.y);

        // ���� ũ�� ����(mapDta.mapSize)�� �������� ī�޶� �þ� ũ�� ����
        cameraController.Setup(mapData.mapSize.x, mapData.mapSize.y);

        // ���� ���������� ������ UI�� ���
        stageUI.UpdateTextStage("Stage01");
    }

    public void GameClear()
    {
        Debug.Log("Game Clear");
    }
}
