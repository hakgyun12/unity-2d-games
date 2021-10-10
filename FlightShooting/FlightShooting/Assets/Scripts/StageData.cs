using UnityEngine;


/* 부모클래스로 ScriptableObject를 사용하면 해당 클래스를 에셋 파일의 형태로
 * 저장할 수 있고, 7번째 줄과 같이 클래스 상단에 CreateAssetMenu를 붙이게 되면 Project View의
 * Create("+") 메뉴에 메뉴로 등록된다.
 */
[CreateAssetMenu]
public class StageData : ScriptableObject
{
    [SerializeField]
    private Vector2 limitMin;
    [SerializeField]
    private Vector2 limitMax;

    public Vector2 LimitMin => limitMin;
    public Vector2 LimitMax => limitMax;
}
