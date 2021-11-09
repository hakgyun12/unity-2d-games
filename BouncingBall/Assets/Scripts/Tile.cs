using UnityEngine;

// Ÿ���� �Ӽ� (�� ����, �⺻, �μ��� �� �ִ� ��ź, ����, ���� ����, ������ ����, �����̵�, ���� �ľǿ�)
public enum TileType { Empty = 0, Base, Broke, Boom, Jump, StraightLeft, StraightRight, Blink, LastIndex}

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Sprite[] images; // Ÿ�Ͽ� ����� �� �ִ� �̹��� �迭
    private SpriteRenderer spriteRenderer; // Ÿ�� �̹��� ������ ���� SpriteRendere
    private TileType tileType; // ���� Ÿ���� �Ӽ�

    public void Setup(TileType tileType)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        TileType = tileType;
    }

    public TileType TileType
    {
        set
        {
            tileType = value;
            spriteRenderer.sprite = images[(int)tileType - 1];
        }
        get => tileType;
    }
}
