using UnityEngine.UI;
using UnityEngine;

public class PlayerHPViewer : MonoBehaviour
{
    [SerializeField]
    private PlayerHP playerHP;
    private Slider sliderHP;

    private void Awake()
    {
        sliderHP = GetComponent<Slider>();
    }

    /// <summary>
    /// Tip. �� ��Ȯ�� ������δ� �̺�Ʈ�� �̿��� ü�� ������ �ٲ� ���� UI ���� ����
    /// </summary>
    private void Update()
    {
        // Slider UI�� ���� ü�� ������ ������Ʈ
        sliderHP.value = playerHP.CurrentHP / playerHP.MaxHP;
    }
    
}