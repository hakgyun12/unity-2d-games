using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    private LayerMask tileLayer;
    private float rayDistance = 0.55f;
    private Vector2 moveDirection = Vector2.right;
    private Direction direction = Direction.Right;

    private Movement2D movement2D;
    private AroundWrap aroundWrap;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        tileLayer = 1 << LayerMask.NameToLayer("Tile");

        movement2D = GetComponent<Movement2D>();
        aroundWrap = GetComponent<AroundWrap>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // 1. ����Ű �Է����� �̵����� ����
        if( Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDirection = Vector2.up;
            direction = Direction.Up;
        }
        else if ( Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDirection = Vector2.left;
            direction = Direction.Left;
        }
        else if ( Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDirection = Vector2.right;
            direction = Direction.Right;
        }
        else if ( Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDirection = Vector2.down;
            direction = Direction.Down;
        }

        // position ��ġ���� direction �������� distance �Ÿ���ŭ layer���� �����ϴ� ������ �߻�
        // �ε����� ������Ʈ�� �����ϸ� hit�� �ε��� ������Ʈ�� ������ �����Ѵ�.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);

        // hit ��ü�� null �˻縦 �� �� ���� ������ hit.transform�� null �˻縦 ��.
        if( hit.transform == null)
        {
            //MoveTo() �Լ��� �̵������� �Ű������� ������ �̵�
            bool movePossible = movement2D.MoveTo(moveDirection);
            if ( movePossible)
            {
                transform.localEulerAngles = Vector3.forward * 90 * (int)direction;
            }
            // �̵� ���� �ܰ����� �����ִٸ�
            aroundWrap.UpdateAroundWrap();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            //������ ȹ�� ó�� (����� �������� �ı��ϱ⸸ �Ѵ�)
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Enemy"))
        {
            // �÷��̾� ĳ������ ü�� ���� �� ó��
            StopCoroutine("OnHit");
            StartCoroutine("OnHit");
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator OnHit()
    {
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.white;
    }
}