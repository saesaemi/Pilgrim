using System.Collections;
using UnityEngine;

// 씬 1개 안에서 스테이지 프리팹을 교체하며 진행
// GameScene의 빈 오브젝트에 부착
public class StageLoader : MonoBehaviour
{
    public static StageLoader Instance { get; private set; }

    [SerializeField] private StageDatabase database;
    [SerializeField] private Transform stageRoot;       // 스테이지 프리팹이 생성될 부모
    [SerializeField] private float transitionDuration = 0.4f;

    private GameObject currentStageInstance;
    private int currentIndex = 0;

    // 현재 스테이지 데이터 외부 접근용
    public StageData CurrentData => database.Get(currentIndex);
    public int CurrentIndex => currentIndex;
    public int StageCount => database.TotalStages;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    //private void Start()
    //{
    //    LoadStage(0);
    //}

    public void LoadStage(int index)
    {
        StartCoroutine(LoadRoutine(index));
    }

    public void LoadNextStage()
    {
        int next = currentIndex + 1;
        if (next >= database.TotalStages)
        {
            Debug.Log("모든 스테이지 클리어!");
            return;
        }
        PlayerPrefs.SetInt("GameManager_Stage", next);
        LoadStage(next);
    }

    public void ReloadCurrentStage()
    {
        LoadStage(currentIndex);
    }

    private IEnumerator LoadRoutine(int index)
    {
        // 이전 스테이지 제거
        if (currentStageInstance != null)
        {
            yield return FadeOut();
            Destroy(currentStageInstance);
        }

        currentIndex = index;
        StageData data = database.Get(index);

        if (data == null || data.StagePrefab == null)
        {
            Debug.LogError($"DAY {index + 1} 프리팹 없음");
            yield break;
        }

        // 새 스테이지 생성
        currentStageInstance = Instantiate(data.StagePrefab, stageRoot);

        // GameManager에 시작 위치 알림
        var spawnPoint = currentStageInstance.GetComponentInChildren<SpawnPoint>();
        if (spawnPoint != null)
            GameManager.Instance.UpdateCheckpoint(spawnPoint.transform.position);
        if (data.IsGuide)
            UIManager.Instance.SetGuide();
        else
            UIManager.Instance.RemoveGuide();

        UIManager.Instance.SetStageText(data.StageTitle, data.StageDesc);
        yield return FadeIn();

        Debug.Log($"DAY {data.DayNumber} 로드 완료: {data.StageTitle}");
    }

    // ── 페이드 연출 ──────────────────────────────

    private IEnumerator FadeOut()
    {
        if (FadeController.Instance != null)
            yield return FadeController.Instance.FadeOut(transitionDuration);
        else
            yield return new WaitForSeconds(transitionDuration);
    }

    private IEnumerator FadeIn()
    {
        // 타이틀 표시
        var data = database.Get(currentIndex);
        if (data != null)
            StageTitleUI.Instance?.Show(data.DayNumber, data.StageTitle, data.IsBuffStage);

        if (FadeController.Instance != null)
            yield return FadeController.Instance.FadeIn(transitionDuration);
        else
            yield return new WaitForSeconds(transitionDuration);
    }
    public void ResetStage()
    {
        if(currentStageInstance != null)
            Destroy(currentStageInstance);
        UIManager.Instance.RemoveGuide();
    }
}
