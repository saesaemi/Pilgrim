using UnityEngine;

// DAY 17 허영의 시장 — 군중 오브젝트: 좌우 왕복하며 플레이어를 밀침
[RequireComponent(typeof(Rigidbody2D))]
public class CrowdObstacle : TrapBase
{
    [SerializeField] private float speed = 80f;
    [SerializeField] private float range = 150f;
    [SerializeField] private float pushForce = 150f; // 위치를 직접 밀어내는 거리(유닛) — PlayerController가 매 프레임 velocity.x를 덮어써서 AddForce로는 체감이 안 됨

    [SerializeField] private float bumpInterval = 0.25f; // 겹쳐 있는 동안 반복 튕김 간격(초)

    private Vector3 startPos;
    private int direction = 1;
    private Rigidbody2D rb;
    private float nextBumpTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        float newX = rb.position.x + direction * speed * Time.fixedDeltaTime;
        float offset = newX - startPos.x;

        // 경계를 넘으면 위치를 경계에 고정하고 방향을 한 번만 반전 (매 프레임 반복 반전 방지)
        if (Mathf.Abs(offset) >= range)
        {
            offset = Mathf.Clamp(offset, -range, range);
            newX = startPos.x + offset;
            direction *= -1;
        }

        rb.MovePosition(new Vector2(newX, rb.position.y));
    }

    // 즉사 아님 — 부딪히면 옆으로 밀려남. 낮고 작게 만들어서 점프로 넘어가는 게 기본 대응이 되도록 함
    protected override void OnPlayerTriggerEnter(PlayerController player)
    {
        Bump(player);
    }

    // 겹쳐 있는 동안에도 일정 간격으로 계속 밀어서 "군중에 치이는" 느낌을 유지
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!isActive) return;
        if (Time.time < nextBumpTime) return;
        if (other.TryGetComponent<PlayerController>(out var player))
            Bump(player);
    }

    private void Bump(PlayerController player)
    {
        if (player.TryGetComponent<Rigidbody2D>(out var playerRb))
        {
            Vector2 pushDir = (player.transform.position - transform.position).normalized;
            playerRb.position += pushDir * pushForce;
        }
        nextBumpTime = Time.time + bumpInterval;
    }
}
