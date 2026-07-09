using System.Collections;
using UnityEngine;

// 스테이지 양쪽 끝에 배치 — 플레이어가 닿으면 반대편으로 이동
public class WrapZone : MonoBehaviour
{
    [SerializeField] private WrapZone destinationZone; // 반대편 WrapZone

    private bool isCooling = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCooling) return;
        if (!other.TryGetComponent<PlayerController>(out var player)) return;
        if (player.IsDead) return;

        // 도착한 반대편 WrapZone도 잠깐 비활성화
        destinationZone?.StartCooldown();

        var rb = other.GetComponent<Rigidbody2D>();
        Vector2 newPos = new Vector2(destinationZone.transform.position.x, rb.position.y);
        rb.position = newPos;

    }

    public void StartCooldown() => StartCoroutine(CooldownRoutine());

    private IEnumerator CooldownRoutine()
    {
        isCooling = true;
        yield return new WaitForSeconds(1f);
        isCooling = false;
    }
}
