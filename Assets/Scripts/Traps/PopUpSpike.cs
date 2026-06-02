using System.Collections;
using UnityEngine;

// 바닥에서 솟아오르는 가시 — 일정 간격으로 올라왔다 내려감
public class PopUpSpike : TrapBase
{
    [SerializeField] private float upDuration = 1f;
    [SerializeField] private float downDuration = 1.5f;
    [SerializeField] private float moveDistance = 1f;
    [SerializeField] private float moveSpeed = 8f;

    private Vector3 downPos;
    private Vector3 upPos;

    private void Start()
    {
        downPos = transform.position;
        upPos   = downPos + Vector3.up * moveDistance;
        Deactivate(); // 처음엔 비활성
        StartCoroutine(CycleRoutine());
    }

    private IEnumerator CycleRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(downDuration);
            yield return MoveToRoutine(upPos);
            Activate();                          // TrapBase: 트리거 활성
            yield return new WaitForSeconds(upDuration);
            Deactivate();                        // TrapBase: 트리거 비활성
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

    // TrapBase 기본 동작(즉사) 그대로 사용
}
