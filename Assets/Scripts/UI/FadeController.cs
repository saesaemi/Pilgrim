using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// 씬 전환 시 페이드 인/아웃 담당
// Canvas > FadePanel (전체 화면 검정 Image) 에 부착
public class FadeController : MonoBehaviour
{
    public static FadeController Instance { get; private set; }

    [SerializeField] private Image fadePanel;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        if (fadePanel != null)
            fadePanel.color = new Color(0, 0, 0, 1f);
    }

    public IEnumerator FadeOut(float duration)
    {
        yield return Fade(0f, 1f, duration);
    }

    public IEnumerator FadeIn(float duration)
    {
        yield return Fade(1f, 0f, duration);
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        if (fadePanel == null) yield break;
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            fadePanel.color = new Color(0, 0, 0, Mathf.Lerp(from, to, t / duration));
            yield return null;
        }
        fadePanel.color = new Color(0, 0, 0, to);
    }
}
