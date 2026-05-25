using UnityEngine;

// DAY 11 겸손의 골짜기 — 올라서면 미끄러지는 발판
public class SlipperyPlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out var player))
            player.SetSlippery(true);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<PlayerController>(out var player))
            player.SetSlippery(false);
    }
}
