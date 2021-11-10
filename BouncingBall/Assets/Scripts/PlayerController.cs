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

        // �÷��̾� ĳ������ ������ �̵�
        movement2D.MoveTo(x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �÷��̾�� �ε��� ������Ʈ�� �±װ� "Item"�̸�
        if (collision.tag.Equals("Item"))
        {
            // �ε��� ������Ʈ ����
            Destroy(collision.gameObject);
        }
    }
}
