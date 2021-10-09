using System.Collections;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveTime = 0.2f;  // ��ĭ �̵��� �ҿ�Ǵ� �ð�
    private bool isMove = false;    // ������Ʈ �̵�/��� ���� ����

    public bool MoveTo(Vector3 moveDirection)
    {
        // �̵����̸� �̵��Լ��� ������� �ʵ��� ��
        if( isMove)
        {
            return false;
        }

        // ���� ��ġ�κ��� �̵��������� 1 ���� �̵��� ��ġ�� �Ű������� �ڷ�ƾ �Լ� ����
        StartCoroutine(SmoothGridMovement(transform.position + moveDirection));

        return true;
    }

    private IEnumerator SmoothGridMovement(Vector2 endPosition)
    {
        Vector2 startPosition = transform.position;
        float percent = 0;

        // moveTime �ð� ���� while() �ݺ��� ȣ��
        // while() �ݺ����� ȣ���ϴ� ���� isMove = true, �ݺ��� ���� �� isMove = false
        isMove = true;
        while ( percent < moveTime)
        {
            percent += Time.deltaTime;
            // startPosition���� endPosition���� moveTime �ð����� �̵�
            transform.position = Vector2.Lerp(startPosition, endPosition, percent / moveTime);

            yield return null;
        }
        isMove = false;
    }
}