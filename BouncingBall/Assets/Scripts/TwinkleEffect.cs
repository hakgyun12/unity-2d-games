using System.Collections;
using UnityEngine;
using TMPro;

public class TwinkleEffect : MonoBehaviour
{
    [SerializeField]
    private float fadeTime; // ���̵� �Ǵ� �ð�
    private TextMeshProUGUI textFade; // ���̵� ȿ���� ���Ǵ� TMPro

    private void Awake()
    {
        textFade = GetComponent<TextMeshProUGUI>();

        // Fade In <-> Out�� �ݺ��ؼ� ��¦�̴� ȿ�� ���
        StartCoroutine(Twinkle());
    }

    private IEnumerator Twinkle()
    {
        while(true)
        {
            yield return StartCoroutine(Fade(1, 0)); // Fade In

            yield return StartCoroutine(Fade(0, 1)); // Fade Out
        }
    }

    private IEnumerator Fade(float start, float end)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            Color color = textFade.color;
            color.a = Mathf.Lerp(start, end, percent);
            textFade.color = color;

            yield return null;
        }
    }
}
