using UnityEngine;

// 일정 간격으로 바위를 생성하는 스포너
public class BoulderSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boulderPrefab;
    [SerializeField] private float interval = 3f;
    [SerializeField] private float firstDelay = 1f;

    private void Start()
    {
        InvokeRepeating(nameof(Spawn), firstDelay, interval);
    }

    private void Spawn()
    {
        if (boulderPrefab == null) return;
        Instantiate(boulderPrefab, transform/*.position, Quaternion.identity*/);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.3f);
    }
}
