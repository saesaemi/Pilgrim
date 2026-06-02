using System.Collections;
using UnityEngine;

// 일정 간격으로 열렸다 닫히는 게이트 — 타이밍 맞춰야 통과 가능
public class TimedGate : TrapBase
{
    [SerializeField] private float openDuration  = 1f;
    [SerializeField] private float closeDuration = 1.5f;
    [SerializeField] private float firstOpenDelay = 0f;

    private Collider2D col;
    private SpriteRenderer sr;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        sr  = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(GateCycleRoutine());
    }

    private IEnumerator GateCycleRoutine()
    {
        yield return new WaitForSeconds(firstOpenDelay);
        while (true)
        {
            SetOpen(true);
            yield return new WaitForSeconds(openDuration);
            SetOpen(false);
            yield return new WaitForSeconds(closeDuration);
        }
    }

    private void SetOpen(bool open)
    {
        col.enabled = !open;
        if (sr != null) sr.color = open ? new Color(1, 1, 1, 0.3f) : Color.white;
    }

    // 닫혀 있을 때 충돌하면 즉사
    protected override void OnPlayerCollisionEnter(PlayerController player)
        => player.Die();
}
