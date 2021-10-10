using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;
    [SerializeField]
    private Vector3 moveDirection = Vector3.zero;

    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    //외부에서 호출해 이동 방향을 설정
    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
