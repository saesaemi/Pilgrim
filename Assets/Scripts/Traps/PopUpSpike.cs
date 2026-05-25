using System.Collections;
using UnityEngine;

// 바닥에서 솟아오르는 가시 — 일정 간격으로 올라왔다 내려감
public class PopUpSpike : MonoBehaviour
{
    [SerializeField] private float upDuration = 1f;    // 올라와 있는 시간
    [SerializeField] private float downDuration = 1.5f; // 내려가 있는 시간
    [SerializeField] private float moveDistance = 1f;   // 이동 거리
    [SerializeField] private float moveSpeed = 8f;

    private Vector3 downPos;
    private Vector3 upPos;
    private bool isUp;
    private Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        downPos = transform.position;
        upPos = downPos + Vector3.up * moveDistance;
        col.enabled = false;
        StartCoroutine(CycleRoutine());
    }

    private IEnumerator CycleRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(downDuration);
            yield return MoveToRoutine(upPos);
            col.enabled = true;
            yield return new WaitForSeconds(upDuration);
            col.enabled = false;
            yield return MoveToRoutine(downPos);
        }
    }

    private IEnumerator MoveToRoutine(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out var player))
            player.Die();
    }
}
