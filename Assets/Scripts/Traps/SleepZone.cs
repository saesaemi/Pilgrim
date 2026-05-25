using UnityEngine;

// DAY 28 마법의 땅 졸음 — 영역 안에서 조작 반전 + 슬로우
public class SleepZone : MonoBehaviour
{
    [SerializeField] private float slowMultiplier = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            player.SetControlsReversed(true);
            player.ApplySlow(slowMultiplier, 99f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
        {
            player.SetControlsReversed(false);
            player.ApplySlow(1f, 0f);
        }
    }
}
