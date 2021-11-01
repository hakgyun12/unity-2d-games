using UnityEngine;

[CreateAssetMenu]
public class TowerTemplate : ScriptableObject
{
    public GameObject towerPrefab; // Ÿ�� ������ ���� ������
    public Weapon[] weapon; // ������ Ÿ��(����)����

    [System.Serializable]
    public struct Weapon
    {
        public Sprite sprite; // �������� Ÿ�� �̹��� (UI)
        public float damage; // ���ݷ�
        public float rate; // ���ݼӵ�
        public float range; // ���� ����
        public int cost; // �ʿ� ��� (0���� : �Ǽ�, 1~���� : ���׷��̵�)
        public int sell; // Ÿ�� �Ǹ� �� ȹ�� ���
    }
}
