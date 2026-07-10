using UnityEngine;

// 한 방향으로 굴러가며 플레이어에게 닿으면 즉사
[RequireComponent(typeof(Rigidbody2D))]
public class RollingBoulder : TrapBase
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private Vector2 direction = Vector2.left;
    [SerializeField] private float destroyDistance = 20f; // 시작 위치에서 이 거리 이상이면 제거

    private Rigidbody2D rb;
    private Vector2 startPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        startPos = rb.position;
        rb.linearVelocity = direction.normalized * speed;

        // 굴러가는 연출 — 회전 속도 자동 설정
        float rotDir = direction.x > 0 ? -1f : 1f;
        rb.angularVelocity = rotDir * speed * 100f;
    }

    private void Update()
    {
        if (Vector2.Distance(rb.position, startPos) >= destroyDistance)
            Destroy(gameObject);
    }

    // TrapBase 기본 동작 — 트리거/콜리전 진입 시 즉사
    protected override void OnPlayerTriggerEnter(PlayerController player)
    {
        player.Die();
        Destroy(gameObject);
    }

    protected override void OnPlayerCollisionEnter(PlayerController player)
    {
        player.Die();
        Destroy(gameObject);
    }
}
