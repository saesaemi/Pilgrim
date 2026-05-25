using System.Collections;
using UnityEngine;

// DAY 15 수다쟁이 — 일정 주기로 화면을 가리는 오브젝트 (UI 오버레이 방식)
public class VisionBlocker : MonoBehaviour
{
    [SerializeField] private GameObject blockOverlay;   // 화면 가리는 이미지 오브젝트 (Canvas Image 등)
    [SerializeField] private float blockDuration = 1.5f;
    [SerializeField] private float clearDuration = 2f;

    private void Start()
    {
        if (blockOverlay != null) blockOverlay.SetActive(false);
        StartCoroutine(BlinkRoutine());
    }

    private IEnumerator BlinkRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(clearDuration);
            if (blockOverlay != null) blockOverlay.SetActive(true);
            yield return new WaitForSeconds(blockDuration);
            if (blockOverlay != null) blockOverlay.SetActive(false);
        }
    }
}
