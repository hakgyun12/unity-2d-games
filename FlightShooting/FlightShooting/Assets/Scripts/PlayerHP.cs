using System.Collections;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxHP = 10; //�ִ� ü��
    private float currentHP;  //���� ü��
    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;

    // maxHP ������ ������ �� �ִ� ������Ƽ (Get�� ����)
    public float MaxHP => maxHP;
    // currentHP ������ ������ �� �ִ� ������Ƽ (Get�� ����)
    //public float CurrentHP => currentHP;
    public float CurrentHP
    {
        set => currentHP = Mathf.Clamp(value, 0, maxHP);
        get => currentHP;
    }

    private void Awake()
    {
        currentHP = maxHP; //���� ü���� �ִ� ü�°� ���� ����
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerController = GetComponent<PlayerController>();
    }

    public void TakeDamage(float damage)
    {
        //���� ü���� damage��ŭ ����
        currentHP -= damage;

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");
        
        //ü�� 0 ���� = �÷��̾� ĳ���� ���
        if (currentHP <= 0)
        {
            //ü���� 0�̸� OnDie() �Լ��� ȣ���ؼ� �׾��� �� ó���� �Ѵ�
            playerController.OnDie();
        }
    }

    private IEnumerator HitColorAnimation()
    {
        //�÷��̾� ������ ����������
        spriteRenderer.color = Color.red;
        //0.1�� ���� ���
        yield return new WaitForSeconds(0.1f);
        //�÷��̾��� ������ ���� ������ �Ͼ������
        // (���� ������ �Ͼ���� �ƴ� ��� ���� ���� ���� ����)
        spriteRenderer.color = Color.white;
    }
}
 