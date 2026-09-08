using UnityEngine;

// DAY 17 허영의 시장 — 군중 오브젝트: 좌우 왕복하며 플레이어를 밀침
[RequireComponent(typeof(Rigidbody2D))]
public class CrowdObstacle : TrapBase
{
    [SerializeField] private float speed = 80f;
    [SerializeField] private float range = 150f;
    [SerializeField] private float pushForce = 150f;

    private Vector3 startPos;
    private int direction = 1;
    private Rigidbody2D rb;

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

    // 즉사 아님 — 통과는 가능하되 부딪히면 밀려남 (완전히 막는 벽이 아님)
    protected override void OnPlayerTriggerEnter(PlayerController player)
    {
        if (player.TryGetComponent<Rigidbody2D>(out var playerRb))
        {
            Vector2 pushDir = (player.transform.position - transform.position).normalized;
            playerRb.AddForce(pushDir * pushForce, ForceMode2D.Impulse);
        }
    }
}
