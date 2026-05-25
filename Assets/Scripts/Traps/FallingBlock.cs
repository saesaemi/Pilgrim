using System.Collections;
using UnityEngine;

// 천장 낙하 블록 — 플레이어가 아래를 지나가면 떨어짐
[RequireComponent(typeof(Rigidbody2D))]
public class FallingBlock : MonoBehaviour
{
    [SerializeField] private float triggerRange = 3f;   // 감지 범위 (아래 방향)
    [SerializeField] private float fallDelay = 0.3f;    // 감지 후 낙하까지 대기
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

        // 아래 방향 레이캐스트로 플레이어 감지
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, triggerRange, playerLayer);
        if (hit.collider != null)
        {
            hasTriggered = true;
            StartCoroutine(FallRoutine());
        }
    }

    private IEnumerator FallRoutine()
    {
        // 깜빡임으로 경고
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
        rb.isKinematic = false; // 중력 활성화 → 낙하
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out var player))
            player.Die();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.down * triggerRange);
    }
}
