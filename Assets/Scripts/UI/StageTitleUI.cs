using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 스테이지 로드 시 "DAY X / 제목" 표시
// Canvas > TitleGroup (CanvasGroup) 안에 dayText, titleText 배치
public class StageTitleUI : MonoBehaviour
{
    public static StageTitleUI Instance { get; private set; }

    [SerializeField] private CanvasGroup titleGroup;
    [SerializeField] private Text dayText;
    [SerializeField] private Text titleText;
    [SerializeField] private float showDuration = 2f;
    [SerializeField] private float fadeDuration = 0.5f;

    private Coroutine currentRoutine;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        if (titleGroup != null)
        {
            titleGroup.alpha = 0f;
            titleGroup.gameObject.SetActive(false);
        }
    }

    public void Show(int day, string title, bool isBuffStage)
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(ShowRoutine(day, title, isBuffStage));
    }

    private IEnumerator ShowRoutine(int day, string title, bool isBuffStage)
    {
        if (dayText   != null) dayText.text   = $"DAY {day}";
        if (titleText != null) titleText.text = isBuffStage ? $"★  {title}" : title;

        titleGroup.gameObject.SetActive(true);
        titleGroup.alpha = 1f;

        yield return new WaitForSeconds(showDuration);

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            titleGroup.alpha = 1f - (t / fadeDuration);
            yield return null;
        }

        titleGroup.alpha = 0f;
        titleGroup.gameObject.SetActive(false);
    }
}
