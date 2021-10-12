using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int damage = 1; // �� ���ݷ�
    [SerializeField]
    private int scorePoint = 100; //�� óġ�� ȹ�� ����
    private PlayerController playerController; //�÷��̾��� ����(Score) ������ �����ϱ� ����
    [SerializeField]
    private GameObject[] itemPrefabs; // ���� �׿��� �� ȹ�� ������ ������
    [SerializeField]
    private GameObject explosionPrefab; //���� ȿ��

    private void Awake()
    {
        // Tip. ���� �ڵ忡���� �ѹ��� ȣ���ϱ� ������ OnDie()���� �ٷ� ȣ���ص� ������
        // ������Ʈ Ǯ���� �̿��� ������Ʈ�� ������ ��쿡�� ���� 1���� Find�� �̿���
        // PlayerController�� ������ �����صΰ� ����ϴ� ���� ���꿡 ȿ�����̴�
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //������ �ε��� ������Ʈ�� �±װ� "Player"�̸�
        if (collision.CompareTag("Player"))
        {
            // �� ���ݷ¸�ŭ �÷��̾� ü�� ����
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            // �� ����� ȣ���ϴ� �Լ�
            OnDie();
        }
    }

    public void OnDie()
    {
        //�÷��̾��� ������ scorePoint��ŭ ������Ų��
        playerController.Score += scorePoint;
        // ���� ����Ʈ ����
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        // ���� Ȯ���� ������ ����
        SpawnItem();
        // �� ������Ʈ ����
        Destroy(gameObject);
    }

    public void SpawnItem()
    {
        int spawnItem = Random.Range(0, 100);
        if (spawnItem < 10)
        {
            Instantiate(itemPrefabs[0], transform.position, Quaternion.identity);
        }
        else if (spawnItem < 15)
        {
            Instantiate(itemPrefabs[1], transform.position, Quaternion.identity);
        }
        else if (spawnItem < 30)
        {
            Instantiate(itemPrefabs[2], transform.position, Quaternion.identity);
        }
    }
}
