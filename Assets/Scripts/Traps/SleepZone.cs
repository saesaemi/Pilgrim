using UnityEngine;

// DAY 28 마법의 땅 졸음 — 영역 안에서 조작 반전 + 슬로우
public class SleepZone : TrapBase
{
    [SerializeField] private float slowMultiplier = 0.5f;

    protected override void OnPlayerTriggerEnter(PlayerController player)
    {
        player.SetControlsReversed(true);
        player.ApplySlow(slowMultiplier, 99f);
    }

    protected override void OnPlayerTriggerExit(PlayerController player)
    {
        player.SetControlsReversed(false);
        player.ApplySlow(1f, 0f);
    }
}
