using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

// 천장 낙하 블록 — 플레이어가 아래를 지나가면 떨어짐
[RequireComponent(typeof(Rigidbody2D))]
public class FallingBlock : TrapBase
{
    [SerializeField] private float triggerRange = 3f;
    [SerializeField] private float fallDelay = 0.1f;
    [SerializeField] private LayerMask playerLayer;

    private Rigidbody2D rb;
    private bool hasTriggered;
    private Vector3 initPos;

    private void Awake()
    {
        initPos = transform.localPosition;
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
        yield return new WaitForSeconds(fallDelay);
        rb.isKinematic = false;
    }

    // 낙하 후 플레이어와 충돌 시 즉사
    protected override void OnPlayerCollisionEnter(PlayerController player)
    {
        player.Die();
        gameObject.SetActive(false);
        rb.isKinematic = true;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.localPosition = initPos;
        transform.localRotation = Quaternion.identity;
        Invoke("Init", 2f);
        //Init();
    }
    protected override void OnOtherCollisionEnter()
        => Destroy(this.gameObject);

    private void Init()
    {
        gameObject.SetActive(true);
        hasTriggered = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, Vector2.down * triggerRange);
    }
}
