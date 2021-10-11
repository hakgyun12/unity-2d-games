using UnityEngine;

public class Meteorite : MonoBehaviour
{
    [SerializeField]
    private int damage = 5; //운석 공격력
    [SerializeField]
    private GameObject explosionPrefab; //폭발 효과

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //적에게 부딪힌 오브젝트의 태그가 "Player"이면
        if (collision.CompareTag("Player"))
        {
            // 운석 공격력만큼 플레이어 체력 감소
            collision.GetComponent<PlayerHP>().TakeDamage(damage);
            // 폭발 이펙트 생성
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            // 적 사망
            Destroy(gameObject);
        }
    }
}
