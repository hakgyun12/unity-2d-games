using UnityEngine;

public class StageController : MonoBehaviour
{
    public static int maxStageCount; // �ִ� �������� ���� (���� ����. Intro ������ ����)

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

        // PlayerPrefs�� �̿��� ����̽��� ����Ǿ� �ִ� ���� �������� �ε��� �ҷ�����
        // �����ϴ� ���� 0���� �����ϰ�, ���������� 1���� �����ϱ� ������ +1
        int index = PlayerPrefs.GetInt("StageIndex") + 1;
        // �ε����� 10���� ������ 01, 02�� ���� 0�� �ٿ��ְ�, 10�̻��̸� �״�� ���
        string currentStage = index < 10 ? $"Stage0{index}" : $"Stage{index}";
        // ���� ����Ǿ� �ִ� json ������ Stage01, Stage02.. json ���� �����͸� �ҷ��´�.
        MapData mapData = mapDataLoader.Load(currentStage);

        // mapData ������ �������� Ÿ�� ������ �� ����
        tilemap2D.GenerateTilemap(mapData);

        // mapData.playerPosition ������ �������� �÷��̾� ��ġ ����
        playerController.Setup(mapData.playerPosition, mapData.mapSize.y);

        // ���� ũ�� ����(mapDta.mapSize)�� �������� ī�޶� �þ� ũ�� ����
        cameraController.Setup(mapData.mapSize.x, mapData.mapSize.y);

        // ���� ���������� ������ UI�� ���
        stageUI.UpdateTextStage(currentStage);
    }

    public void GameClear()
    {
        //Debug.Log("Game Clear");

        // ���� �������� �ε��� ������ ����̽� "StageIndex" Ű�κ��� �ҷ��´�
        int index = PlayerPrefs.GetInt("StageIndex");

        // �ҷ��� index ������ "�ִ� �������� ����-1"���� ������
        // ���� ���� ���������� �����ֱ� ������[
        if (index < maxStageCount-1)
        {
            // index�� 1 ������Ű��, ������ index ���� ����̽��� "StageIndex"Ű�� ����
            index++;
            PlayerPrefs.SetInt("StageIndex", index);

            // ���� ���� �ٽ� �ε�
            // ���� �ٽ� �ε�� �� StageIndex ���� �ٲ���� ������ ���� ���������� .json ������ ���� �ȴ�
            SceneLoader.LoadScene();
        }
        // ������ ���������� Ŭ���� �� ���
        else
        {
            // "Intro" ������ ���ư���( ������ ������ ���� ���� �ε��ϵ��� ����
            SceneLoader.LoadScene("Intro");
        }
    }
}
