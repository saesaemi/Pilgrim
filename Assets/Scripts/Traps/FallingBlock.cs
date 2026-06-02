using System.Collections;
using UnityEngine;

// 천장 낙하 블록 — 플레이어가 아래를 지나가면 떨어짐
[RequireComponent(typeof(Rigidbody2D))]
public class FallingBlock : TrapBase
{
    [SerializeField] private float triggerRange = 3f;
    [SerializeField] private float fallDelay = 0.3f;
    [SerializeField] private LayerMask playerLayer;

    private Rigidbody2D rb;
    private bool hasTriggered;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    private void Update()
    {
        if (hasTriggered) return;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, triggerRange, playerLayer);
        if (hit.collider != null)
        {
            hasTriggered = true;
            StartCoroutine(FallRoutine());
        }
    }

    private IEnumerator FallRoutine()
    {
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            for (int i = 0; i < 2; i++)
            {
                sr.color = Color.red;
                yield return new WaitForSeconds(0.1f);
                sr.color = Color.white;
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return new WaitForSeconds(fallDelay);
        rb.isKinematic = false;
    }

    // 낙하 후 플레이어와 충돌 시 즉사
    protected override void OnPlayerCollisionEnter(PlayerController player)
        => player.Die();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.down * triggerRange);
    }
}
