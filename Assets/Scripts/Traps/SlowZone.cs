using UnityEngine;

// DAY 2 낙담의 수렁 — 영역 안에서 이동 속도 감소
public class SlowZone : MonoBehaviour
{
    [SerializeField] private float slowMultiplier = 0.4f;
    [SerializeField] private float duration = 99f; // 존 안에 있는 동안 지속

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
            player.ApplySlow(slowMultiplier, duration);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
            player.ApplySlow(1f, 0f); // 슬로우 해제
    }
}
