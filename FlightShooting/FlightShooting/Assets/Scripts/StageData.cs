using UnityEngine;


/* �θ�Ŭ������ ScriptableObject�� ����ϸ� �ش� Ŭ������ ���� ������ ���·�
 * ������ �� �ְ�, 7��° �ٰ� ���� Ŭ���� ��ܿ� CreateAssetMenu�� ���̰� �Ǹ� Project View��
 * Create("+") �޴��� �޴��� ��ϵȴ�.
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
