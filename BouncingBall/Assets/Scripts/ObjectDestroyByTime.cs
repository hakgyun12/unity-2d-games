using UnityEngine;

public class ObjectDestroyByTime : MonoBehaviour
{
    [SerializeField]
    private float destroyTime;

    private void Awake()
    {
        // destroyTime �ð� �ڿ� gameObject ����
        Destroy(gameObject, destroyTime);
    }
}
