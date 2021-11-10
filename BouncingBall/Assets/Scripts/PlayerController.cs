using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Movement2D movement2D;

    public void Setup(Vector2Int position)
    {
        movement2D = GetComponent<Movement2D>();

        transform.position = new Vector3(position.x, position.y, 0);
    }

    private void Update()
    {
        UpdateMove();
    }

    private void UpdateMove()
    {
        float x = Input.GetAxisRaw("Horizontal");

        // 플레이어 캐릭터의 물리적 이동
        movement2D.MoveTo(x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어에게 부딪힌 오브젝트의 태그가 "Item"이면
        if (collision.tag.Equals("Item"))
        {
            // 부딪힌 오브젝트 삭제
            Destroy(collision.gameObject);
        }
    }
}
