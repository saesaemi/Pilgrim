using UnityEngine;

// DAY 17 허영의 시장 — 군중 오브젝트: 좌우 왕복하며 플레이어를 밀침
[RequireComponent(typeof(Rigidbody2D))]
public class CrowdObstacle : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float range = 3f;

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 플레이어를 옆으로 밀어냄 (즉사 아님 — 밀려서 낙사 유도)
        if (other.gameObject.TryGetComponent<Rigidbody2D>(out var playerRb))
        {
            Vector2 pushDir = (other.transform.position - transform.position).normalized;
            playerRb.AddForce(pushDir * 8f, ForceMode2D.Impulse);
        }
    }
}
