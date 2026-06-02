using UnityEngine;

// DAY 17 허영의 시장 — 군중 오브젝트: 좌우 왕복하며 플레이어를 밀침
[RequireComponent(typeof(Rigidbody2D))]
public class CrowdObstacle : TrapBase
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float range = 3f;
    [SerializeField] private float pushForce = 8f;

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
        rb.MovePosition(rb.position + Vector2.right * direction * speed * Time.fixedDeltaTime);

        if (Mathf.Abs(transform.position.x - startPos.x) >= range)
            direction *= -1;
    }

    // 즉사 아님 — 밀려서 낙사 유도
    protected override void OnPlayerCollisionEnter(PlayerController player)
    {
        if (player.TryGetComponent<Rigidbody2D>(out var playerRb))
        {
            Vector2 pushDir = (player.transform.position - transform.position).normalized;
            playerRb.AddForce(pushDir * pushForce, ForceMode2D.Impulse);
        }
    }
}
