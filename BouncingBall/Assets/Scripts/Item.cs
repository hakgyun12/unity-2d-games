using UnityEngine;

// �������� �Ӽ� (����)
public enum ItemType { Coin = 10 }

public class Item : MonoBehaviour
{
    [SerializeField]
    private GameObject itemEffectPrefab; // ������ ȹ�� ����Ʈ ������

    public void Exit()
    {
        // �������� ����� �� ������ ȹ�� ����Ʈ ����
        Instantiate(itemEffectPrefab, transform.position, Quaternion.identity);

        // ������ ������Ʈ ����
        Destroy(gameObject);
    }
}
