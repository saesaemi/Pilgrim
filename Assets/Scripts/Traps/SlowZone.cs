using UnityEngine;

// DAY 2 낙담의 수렁 — 영역 안에서 이동 속도 감소
public class SlowZone : TrapBase
{
    [SerializeField] private float slowMultiplier = 0.4f;

    protected override void OnPlayerTriggerEnter(PlayerController player)
        => player.ApplySlow(slowMultiplier, 99f);

    protected override void OnPlayerTriggerExit(PlayerController player)
        => player.ApplySlow(1f, 0f);
}
