using UnityEngine;

public class AroundWrap : MonoBehaviour
{
    [SerializeField]
    private StageData stageData;

    public void UpdateAroundWrap()
    {
        // ����Ƽ�� Transform Ŭ������ �ִ� position�� ������Ƽ�̱� ������ x, y, z ���� ������ set �Ұ���
        Vector3 position = transform.position;

        // ���� ���̳� ������ ���� �����ϸ� �ݴ������� �̵�
        if ( position.x < stageData.LimitMin.x)
        {
            position.x = stageData.LimitMax.x;
        }

        if ( position.x > stageData.LimitMax.x)
        {
            position.x = stageData.LimitMin.x;
        }

        // ���� ���̳� �Ʒ��� ���� �����ϸ� �ݴ������� �̵�
        if ( position.y < stageData.LimitMin.y || position.y > stageData.LimitMax.y )
        {
            position.y *= -1;
        }

        transform.position = position;
    }
}
