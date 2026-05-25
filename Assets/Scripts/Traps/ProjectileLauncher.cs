using UnityEngine;

// 일정 간격으로 투사체를 발사하는 발사대
public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float interval = 2f;
    [SerializeField] private Vector2 direction = Vector2.left;
    [SerializeField] private float firstDelay = 0f;

    private void Start()
    {
        InvokeRepeating(nameof(Launch), firstDelay, interval);
    }

    private void Launch()
    {
        if (projectilePrefab == null) return;
        var go = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        if (go.TryGetComponent<Projectile>(out _))
        {
            // direction은 Projectile의 Inspector에서 설정하거나 여기서 주입
            var rb = go.GetComponent<Rigidbody2D>();
            if (rb != null) rb.linearVelocity = direction.normalized * 6f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direction.normalized * 2f);
    }
}
