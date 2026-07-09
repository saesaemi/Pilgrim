using UnityEngine;

// 모든 함정의 베이스 클래스
// Trigger/Collision Enter·Exit 각각에 대한 가상 메서드를 제공
// 기본 동작: 트리거 진입 시 플레이어 즉사
public class TrapBase : MonoBehaviour
{
    [SerializeField] protected bool isActive = true;

    // ── Unity 이벤트 (봉인) ──────────────────────

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive) return;
        if (other.TryGetComponent<PlayerController>(out var player))
            OnPlayerTriggerEnter(player);
        else
            OnOtherTriggerEnter();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isActive) return;
        if (other.TryGetComponent<PlayerController>(out var player))
            OnPlayerTriggerExit(player);
        else 
            OnOtherTriggerExit();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!isActive) return;
        if (other.gameObject.TryGetComponent<PlayerController>(out var player))
            OnPlayerCollisionEnter(player);
        else
            OnOtherCollisionEnter();
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (!isActive) return;
        if (other.gameObject.TryGetComponent<PlayerController>(out var player))
            OnPlayerCollisionExit(player);
        else
            OnOtherCollisionExit();
    }

    // ── 오버라이드용 가상 메서드 ─────────────────

    // 트리거 진입 — 기본: 즉사
    protected virtual void OnPlayerTriggerEnter(PlayerController player) => player.Die();

    // 트리거 퇴장 — 기본: 아무것도 안 함
    protected virtual void OnPlayerTriggerExit(PlayerController player) { }

    // 콜리전 진입 — 기본: 아무것도 안 함
    protected virtual void OnPlayerCollisionEnter(PlayerController player) { }

    // 콜리전 퇴장 — 기본: 아무것도 안 함
    protected virtual void OnPlayerCollisionExit(PlayerController player) { }

    // 다른 물체 트리거 진입 - 기본 : 아무것도 안함
    protected virtual void OnOtherTriggerEnter() { }

    // 다른 물체 트리거 퇴장 — 기본: 아무것도 안 함
    protected virtual void OnOtherTriggerExit() { }

    // 다른 물체 콜리전 진입 — 기본: 아무것도 안 함
    protected virtual void OnOtherCollisionEnter() { }

    // 다른 물체 콜리전 퇴장 — 기본: 아무것도 안 함
    protected virtual void OnOtherCollisionExit() { }

    // ── 활성화 제어 ──────────────────────────────

    public virtual void Activate()   => isActive = true;
    public virtual void Deactivate() => isActive = false;
}
