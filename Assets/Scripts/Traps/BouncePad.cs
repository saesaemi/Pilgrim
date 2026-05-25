using UnityEngine;

// 예상치 못한 방향으로 튕겨내는 발판 (트롤 요소)
public class BouncePad : MonoBehaviour
{
    [SerializeField] private Vector2 bounceForce = new Vector2(0f, 20f);
    [SerializeField] private bool overrideXVelocity = false;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.TryGetComponent<Rigidbody2D>(out var rb)) return;

        Vector2 vel = rb.linearVelocity;
        if (overrideXVelocity)
            vel.x = bounceForce.x;
        else
            vel.x += bounceForce.x;

        vel.y = bounceForce.y;
        rb.linearVelocity = vel;
    }
}
