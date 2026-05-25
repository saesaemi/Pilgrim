using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckRadius = 0.1f;

    // 상태이상
    private float speedMultiplier = 1f;     // 슬로우 등 속도 배율
    private bool isControlsReversed = false; // 조작 반전 (마법의 땅)
    private bool isSlippery = false;         // 미끄러운 바닥

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isDead;

    public bool IsDead => isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isDead) return;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        float horizontal = Input.GetAxisRaw("Horizontal");
        if (isControlsReversed) horizontal *= -1f;

        float targetVelX = horizontal * moveSpeed * speedMultiplier;

        if (isSlippery)
            rb.linearVelocity = new Vector2(Mathf.Lerp(rb.linearVelocity.x, targetVelX, Time.deltaTime * 3f), rb.linearVelocity.y);
        else
            rb.linearVelocity = new Vector2(targetVelX, rb.linearVelocity.y);

        bool jumpInput = isControlsReversed ? Input.GetButtonDown("Fire1") : Input.GetButtonDown("Jump");
        if (jumpInput && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce * speedMultiplier);
        }
    }

    // ── 상태이상 API ──────────────────────────────

    public void ApplySlow(float multiplier, float duration)
    {
        StartCoroutine(SlowRoutine(multiplier, duration));
    }

    private IEnumerator SlowRoutine(float multiplier, float duration)
    {
        speedMultiplier = multiplier;
        yield return new WaitForSeconds(duration);
        speedMultiplier = 1f;
    }

    public void SetControlsReversed(bool reversed) => isControlsReversed = reversed;
    public void SetSlippery(bool slippery) => isSlippery = slippery;

    // ── 죽음 / 리스폰 ─────────────────────────────

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        speedMultiplier = 1f;
        isControlsReversed = false;
        isSlippery = false;
        rb.linearVelocity = Vector2.zero;
        GameManager.Instance.OnPlayerDied();
    }

    public void Respawn(Vector3 position)
    {
        transform.position = position;
        rb.linearVelocity = Vector2.zero;
        speedMultiplier = 1f;
        isControlsReversed = false;
        isSlippery = false;
        isDead = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
