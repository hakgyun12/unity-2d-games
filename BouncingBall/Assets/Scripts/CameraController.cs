using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float wDelta = 0.35f;
    private float hDelta = 0.6f;

    public void Setup(int width, int height)
    {
        // ī�޶� �þ� ���� ( ��ü ���� ȭ�� �ȿ� �������� ���̸� �������� ����)
        float size = (width > height) ? width * wDelta : height * hDelta;

        // ī�޶� �þ� ũ�� ����
        GetComponent<Camera>().orthographicSize = size;
    }
}
