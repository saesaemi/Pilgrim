using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public PlayerController Player;
    public Transform DefaultRespawnPoint;
    public float RespawnDelay = 1.5f;
    public float VictoryDelay = 0.5f;

    private Vector3 currentCheckpoint;
    private int deathCount;

    public int DeathCount => deathCount;
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public bool IsPause = false;
    public bool IsTestScene = false;
    public int SaveStageData => PlayerPrefs.GetInt("GameManager_Stage", 0);
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this);
        currentCheckpoint = DefaultRespawnPoint != null ? DefaultRespawnPoint.position : Vector3.zero;
        if(IsTestScene == false) GotoIntro();
    }
    public void UpdateCheckpoint(Vector3 position)
    {
        currentCheckpoint = position;
        Player.gameObject.SetActive(true);
        Player.Respawn(currentCheckpoint);
        IsPause = false;
        Debug.Log($"체크포인트 갱신: {position}");
    }

    public void OnPlayerDied()
    {
        deathCount++;
        Debug.Log($"사망 #{deathCount}");
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        yield return new WaitForSeconds(RespawnDelay);
        Player.Respawn(currentCheckpoint);
    }
    public void OnClearStage(float time)
    {
        IsPause = true;
        Player.gameObject.SetActive(false);
        StartCoroutine(VictoryRoutine(time));
    }
    private IEnumerator VictoryRoutine(float time)
    {
        Player.Victory();
        yield return new WaitForSeconds(time);
        LoadNextStage();
    }

    // StageLoader 방식 — 씬 전환 없이 다음 스테이지로
    public void LoadNextStage()
    {
        StageLoader.Instance?.LoadNextStage();
    }

    public void ReloadCurrentStage()
    {
        // 리스폰 포인트 초기화 후 스테이지 재로드
        StageLoader.Instance?.ReloadCurrentStage();
    }
    public void GotoIntro()
    {
        Player.gameObject.SetActive(false);
        UIManager.Instance?.Get(UIManager.UIType.INTRO);
    }
    public void GotoSelectStage()
    {
        Player.gameObject.SetActive(false);
        UIManager.Instance?.Get(UIManager.UIType.SELECTSTAGE);
    }
}
