using UnityEngine;

// 일정 간격으로 투사체를 발사하는 발사대
public class ProjectileLauncher : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float interval = 2f;
    [SerializeField] private Vector2 direction = Vector2.left;
    [SerializeField] private float launchSpeed = 100f;
    [SerializeField] private float firstDelay = 0f;

    private void Start()
    {
        InvokeRepeating(nameof(Launch), firstDelay, interval);
    }

    private void Launch()
    {
        if (projectilePrefab == null) return;
        if (GameManager.Instance.IsPause) return;
            var go = Instantiate(projectilePrefab, transform/*, Quaternion.identity*/);
        if (go.TryGetComponent<Projectile>(out var projectile))
            projectile.Launch(direction, launchSpeed);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, direction.normalized * 2f);
    }
}
