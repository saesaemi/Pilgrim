using UnityEngine;

// DAY 4 좁은 문 — 날아오는 투사체
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : TrapBase
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private Vector2 direction = Vector2.left;
    [SerializeField] private float lifeTime = 5f;

    private bool launchedExternally;

    private void Start()
    {
        if (!launchedExternally)
        {
            GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * speed;
            FaceDirection(direction);
        }
        Destroy(gameObject, lifeTime);
    }

    // 발사대(ProjectileLauncher)가 직접 방향/속도를 지정할 때 사용 — Start()의 기본값을 덮어쓰지 않도록 함
    public void Launch(Vector2 launchDirection, float launchSpeed)
    {
        launchedExternally = true;
        GetComponent<Rigidbody2D>().linearVelocity = launchDirection.normalized * launchSpeed;
        FaceDirection(launchDirection);
    }

    // 스프라이트가 기본적으로 오른쪽을 향하고 있다고 가정 — 왼쪽으로 이동할 때 좌우 반전
    private void FaceDirection(Vector2 dir)
    {
        if (Mathf.Approximately(dir.x, 0f)) return;
        var scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * Mathf.Sign(dir.x);
        transform.localScale = scale;
    }

    protected override void OnPlayerTriggerEnter(PlayerController player)
    {
        player.Die();
        Destroy(gameObject);
    }
}
