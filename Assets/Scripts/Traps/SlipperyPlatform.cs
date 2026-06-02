using UnityEngine;

// DAY 11 겸손의 골짜기 — 올라서면 미끄러지는 발판
public class SlipperyPlatform : TrapBase
{
    protected override void OnPlayerCollisionEnter(PlayerController player)
        => player.SetSlippery(true);

    protected override void OnPlayerCollisionExit(PlayerController player)
        => player.SetSlippery(false);
}
