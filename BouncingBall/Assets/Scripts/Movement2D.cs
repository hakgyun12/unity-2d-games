using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [Header("Raycast Collision")]
    [SerializeField]
    private LayerMask collisionLayer; // ������ �ε����� �浹 ���̾�

    [Header("Raycast")]
    [SerializeField]
    private int horizontalRayCount = 4; // x�� �������� �߻�Ǵ� ������ ����
    [SerializeField]
    private int verticalRayCount = 4; // y�� �������� �߻�Ǵ� ������ ����

    private float horizontalRaySpacing; // x�� ������ ���� ���� ����
    private float verticalRaySpacing; // y�� ������ ���� ���� ����

    [Header("Movement")]
    [SerializeField]
    private float moveSpeed; // �̵��ӵ�
    [SerializeField]
    private float jumpForce = 10; // ���� ��
    private float gravity = -20.0f; // �߷�

    private Vector3 velocity; // ������Ʈ �ӷ�
    private readonly float skinWidth = 0.015f; // ������Ʈ �������� �İ��� �ҷ��� ����

    private Collider2D collider2D_1; // ���� �߻� ��ġ ������ ���� �浹 ����
    private ColliderCorner colliderCorner; // ���� �߻縦 ���� �𼭸� ��
    private CollisionChecker collisionChecker; // 4���� �浹 ���� üũ

    private void Awake()
    {
        collider2D_1 = GetComponent<Collider2D>();
    }

    private void Update()
    {
        CalculateRaySpacing(); // ���� ������ �Ÿ� ����
        UpdateColliderCorner(); // �浹 ������ �ܰ� ��ġ ����
        collisionChecker.Reset(); // �浹 ���� �ʱ�ȭ (��/�Ʒ�/��/��)

        // �̵� ������Ʈ
        UpdateMovement();

        // õ���̳� �ٴڿ� ������ velocity.y ���� 0���� ����
        if( collisionChecker.up || collisionChecker.down)
        {
            velocity.y = 0;
        }

        // ���� ������Ʈ (�ٴ��� ������ �ٷ� ������ �ǵ��� JumpTo()�� ��� ȣ��)
        // ���� Ÿ�Ͽ��� ȣ���ϰ� ��
        JumpTo();
    }

    private void UpdateMovement()
    {
        // �߷� ����
        velocity.y += gravity * Time.deltaTime;

        // ���� �����ӿ� ����� ���� �ӷ�
        Vector3 currentVelocity = velocity * Time.deltaTime;

        // �ӷ��� 0�� �ƴ� �� ������ �߻��� �̵� ���� ���� ����
        if( currentVelocity.x != 0 )
        {
            RaycastsHorizontal(ref currentVelocity);
        }
        if (currentVelocity.y != 0)
        {
            RaycastsVertical(ref currentVelocity);
        }

        // ������Ʈ �̵�
        transform.position += currentVelocity;
    }

    /// <summary>
    /// Ÿ Ŭ�������� ȣ��(x�� �̵� �ൿ)
    /// </summary>
    public void MoveTo(float x)
    {
        // x�� ���� �ӷ��� x * moveSpeed�� ����
        velocity.x = x * moveSpeed;
    }

    /// <summary>
    /// Ÿ Ŭ�������� ȣ��(���� �ൿ)
    /// </summary>
    public void JumpTo()
    {
        // �ٴڿ� ���������
        if (collisionChecker.down)
        {
            // y�� �ӷ��� jumpForce�� ������ ����!
            velocity.y = jumpForce;
        }
    }

    /// <summary>
    /// x�� �������� ���� �߻�(��/�� �̵�)
    /// </summary>
    public void RaycastsHorizontal(ref Vector3 velocity)
    {
        float direction = Mathf.Sign(velocity.x); // �̵����� (��:1, ��:-1)
        float distance = Mathf.Abs(velocity.x) + skinWidth; // ���� ����
        Vector2 rayPosition = Vector2.zero; // ������ �߻�Ǵ� ��ġ
        RaycastHit2D hit;

        for (int i = 0; i < horizontalRayCount; ++i)
        {
            rayPosition = (direction == 1) ? colliderCorner.bottomRight : colliderCorner.bottomLeft;
            rayPosition += Vector2.up * (horizontalRaySpacing * i);

            hit = Physics2D.Raycast(rayPosition, Vector2.right * direction, distance, collisionLayer);

            if (hit)
            {
                //x�� �ӷ��� ������ ������Ʈ ������ �Ÿ��� ����( �Ÿ��� 0�̸� �ӷ� 0)
                velocity.x = (hit.distance - skinWidth) * direction;

                //������ �߻�Ǵ� ������ �Ÿ� ����
                distance = hit.distance;

                // ���� �������, �ε��� ������ ������ true�� ����
                collisionChecker.left = direction == -1;
                collisionChecker.right = direction == 1;
            }

            // Debug : �߻�Ǵ� ������ Scene View���� Ȯ��
            Debug.DrawLine(rayPosition, rayPosition + Vector2.right * direction * distance, Color.yellow);
        }
    }

    /// <summary>
    /// y�� �������� ���� �߻�(��/�� �̵�)
    /// </summary>
    public void RaycastsVertical(ref Vector3 velocity)
    {
        float direction = Mathf.Sign(velocity.y); // �̵����� (��:1, ��:-1)
        float distance = Mathf.Abs(velocity.y) + skinWidth; // ���� ����
        Vector2 rayPosition = Vector2.zero; // ������ �߻�Ǵ� ��ġ
        RaycastHit2D hit;

        for (int i = 0; i < verticalRayCount; ++i)
        {
            rayPosition = (direction == 1) ? colliderCorner.topLeft : colliderCorner.bottomLeft;
            rayPosition += Vector2.right * (verticalRaySpacing * i + velocity.x);

            hit = Physics2D.Raycast(rayPosition, Vector2.up * direction, distance, collisionLayer);

            if (hit)
            {
                //x�� �ӷ��� ������ ������Ʈ ������ �Ÿ��� ����( �Ÿ��� 0�̸� �ӷ� 0)
                velocity.y = (hit.distance - skinWidth) * direction;

                //������ �߻�Ǵ� ������ �Ÿ� ����
                distance = hit.distance;

                // ���� �������, �ε��� ������ ������ true�� ����
                collisionChecker.down = direction == -1;
                collisionChecker.up = direction == 1;
            }

            // Debug : �߻�Ǵ� ������ Scene View���� Ȯ��
            Debug.DrawLine(rayPosition, rayPosition + Vector2.up * direction * distance, Color.yellow);
        }
    }


    /// <summary>
    /// �� ������ �߻�Ǵ� ���� ������ ���� (���� ������ ���� �޶���)
    /// </summary>
    private void CalculateRaySpacing()
    {
        Bounds bounds = collider2D_1.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    /// <summary>
    /// �浹 ����(Collider)�� �ܰ� �� ��ġ
    /// </summary>
    private void UpdateColliderCorner()
    {
        // ���� ������Ʈ�� ��ġ �������� Collider�� ������ �޾ƿ�
        Bounds bounds = collider2D_1.bounds;
        bounds.Expand(skinWidth * -2);

        colliderCorner.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        colliderCorner.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        colliderCorner.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
    }

    private struct ColliderCorner
    {
        public Vector2 topLeft;
        public Vector2 bottomLeft;
        public Vector2 bottomRight;
    }

    /// <summary>
    /// ��, ��, ��, �� �浹 ���� ����
    /// </summary>
    public struct CollisionChecker
    {
        public bool up;
        public bool down;
        public bool left;
        public bool right;

        public void Reset()
        {
            up = false;
            down = false;
            left = false;
            right = false;
        }
    }

    private void OnDrawGizmos()
    {
        // �׷����� ������ ����
        Gizmos.color = Color.blue;
        // ��, �쿡 ǥ�õǴ� ���� �߻� ��ġ
        for (int i = 0; i< horizontalRayCount; ++i)
        {
            Vector2 position = Vector2.up * horizontalRaySpacing * i;
            // �� ������ ������ �׸�(��ġ, ������)
            Gizmos.DrawSphere(colliderCorner.bottomRight + position, 0.1f);
            Gizmos.DrawSphere(colliderCorner.bottomLeft + position, 0.1f);
        }
        // ��, �Ʒ��� ǥ�õǴ� ���� �߻� ��ġ
        for (int i = 0; i< verticalRayCount; ++i)
        {
            Vector2 position = Vector2.right * verticalRaySpacing * i;
            // �� ������ ������ �׸� (��ġ, ������)
            Gizmos.DrawSphere(colliderCorner.topLeft + position, 0.1f);
            Gizmos.DrawSphere(colliderCorner.bottomLeft + position, 0.1f);
        }
    }
}
