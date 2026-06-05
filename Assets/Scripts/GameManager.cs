using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public PlayerController Player;
    public Transform DefaultRespawnPoint;
    public float RespawnDelay = 1.5f;
    public float VictoryDelay = 1f;

    private Vector3 currentCheckpoint;
    private int deathCount;

    public int DeathCount => deathCount;
    private static GameManager _instance;
    public static GameManager Instance => _instance;

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
        UIManager.Instance?.Get(UIManager.UIType.INTRO);
    }
    public void UpdateCheckpoint(Vector3 position)
    {
        currentCheckpoint = position;
        Player.Respawn(currentCheckpoint);
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
    public void OnClearStage(Vector3 positioin)
    {
        StartCoroutine(VictoryRoutine(positioin));
    }
    private IEnumerator VictoryRoutine(Vector3 positioin)
    {
        Player.Victory(positioin);
        yield return new WaitForSeconds(VictoryDelay);
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
}
