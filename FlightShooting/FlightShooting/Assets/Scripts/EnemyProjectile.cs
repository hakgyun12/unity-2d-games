using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    [SerializeField]
    private GameObject explosionPrefab; //���� ȿ��

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �߻�ü�� �ε��� ������Ʈ�� �±װ� "Player"�̸�
        if(collision.CompareTag("Player"))
        {
            // �ε��� ������Ʈ ü�� ���� (�÷��̾�)
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            // �� ������Ʈ ����(�߻�ü)
            Destroy(gameObject);
        }
    }

    ///<summary>
    /// ��ź�� ���� �߻�ü�� ���� �ɶ� ȣ��Ǵ� �Լ��� �߻�ü ���� ȿ���� �����ش�.
    /// </summary>
    public void OnDie()
    {
        // ���� ȿ�� ����
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        // ��/���� �߻�ü ����
        Destroy(gameObject);
    }
}
