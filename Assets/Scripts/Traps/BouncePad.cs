using UnityEngine;

// 예상치 못한 방향으로 튕겨내는 발판 (트롤 요소)
public class BouncePad : TrapBase
{
    [SerializeField] private Vector2 bounceForce = new Vector2(0f, 20f);
    [SerializeField] private bool overrideXVelocity = false;

    protected override void OnPlayerCollisionEnter(PlayerController player)
    {
        if (!player.TryGetComponent<Rigidbody2D>(out var rb)) return;

        Vector2 vel = rb.linearVelocity;
        vel.x = overrideXVelocity ? bounceForce.x : vel.x + bounceForce.x;
        vel.y = bounceForce.y;
        rb.linearVelocity = vel;
    }
}
