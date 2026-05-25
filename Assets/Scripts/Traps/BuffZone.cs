using UnityEngine;

// ★ 버프 구간 — 통과 시 리스폰 포인트 갱신 + 상태이상 해제
public class BuffZone : MonoBehaviour
{
    [SerializeField] private bool updateCheckpoint = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<PlayerController>(out var player)) return;

        // 상태이상 해제
        player.SetControlsReversed(false);
        player.SetSlippery(false);
        player.ApplySlow(1f, 0f);

        // 체크포인트 갱신
        if (updateCheckpoint)
            GameManager.Instance.UpdateCheckpoint(transform.position);

        // 한 번만 작동
        gameObject.SetActive(false);
    }
}
