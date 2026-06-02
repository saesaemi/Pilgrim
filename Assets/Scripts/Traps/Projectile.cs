using UnityEngine;

// DAY 4 좁은 문 — 날아오는 투사체
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : TrapBase
{
    [SerializeField] private float speed = 6f;
    [SerializeField] private Vector2 direction = Vector2.left;
    [SerializeField] private float lifeTime = 5f;

    private void Start()
    {
        GetComponent<Rigidbody2D>().linearVelocity = direction.normalized * speed;
        Destroy(gameObject, lifeTime);
    }

    protected override void OnPlayerTriggerEnter(PlayerController player)
    {
        player.Die();
        Destroy(gameObject);
    }
}
