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
        // 1. 방향키 입력으로 이동방향 설정
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

        // position 위치에서 direction 방향으로 distance 거리만큼 layer에만 반응하는 광선을 발사
        // 부딪히는 오브젝트가 존재하면 hit에 부딪힌 오브젝트의 정보를 저장한다.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);

        // hit 자체는 null 검사를 할 수 없기 때문에 hit.transform의 null 검사를 함.
        if( hit.transform == null)
        {
            //MoveTo() 함수에 이동방향을 매개변수로 전달해 이동
            bool movePossible = movement2D.MoveTo(moveDirection);
            if ( movePossible)
            {
                transform.localEulerAngles = Vector3.forward * 90 * (int)direction;
            }
            // 이동 이후 외곽으로 나가있다면
            aroundWrap.UpdateAroundWrap();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            //아이템 획득 처리 (현재는 아이템을 파괴하기만 한다)
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Enemy"))
        {
            // 플레이어 캐릭터의 체력 감소 등 처리
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