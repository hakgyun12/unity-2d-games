using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using TMPro;

public class MapDataSave : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputFileName;
    [SerializeField]
    private Tilemap2D tilemap2D;

    private void Awake()
    {
        inputFileName.text = "NoName.json";
    }

    public void Save()
    {
        // tilemap2D�� ����Ǿ� �ִ� MapData ������ �ҷ��´�.
        // �� ũ��, �÷��̾� ĳ���� ��ġ, �ʿ� �����ϴ� Ÿ�ϵ��� ����
        MapData mapData = tilemap2D.GetMapData();

        // inputField UI�� �Էµ� �ؽ�Ʈ ������ �ҷ��� fileName�� ����
        string fileName = inputFileName.text;
        // fileName�� ".json" ������ ������ �Է����ش�.
        // ex) "Stage01" => "Stage01.json"
        if(fileName.Contains(".json") == false)
        {
            fileName += ".json";
        }
        // ������ ���, ���ϸ��� �ϳ��� ��ĥ �� ���
        // ���� ������Ʈ ��ġ �������� "MapData" ����
        fileName = Path.Combine("MapData/", fileName);

        // mapData �ν��Ͻ��� �ִ� ������ ����ȭ�ؼ� toJson ������ ���ڿ� ���·� ����
        string toJson = JsonConvert.SerializeObject(mapData, Formatting.Indented);
        // "fileName" ���Ͽ� "toJson" ������ ����
        File.WriteAllText(fileName, toJson);
    }
}
