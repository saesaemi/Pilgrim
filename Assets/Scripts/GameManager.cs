using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private PlayerController player;
    [SerializeField] private Transform defaultRespawnPoint;
    [SerializeField] private float respawnDelay = 1f;

    private Vector3 currentCheckpoint;
    private int deathCount;

    public int DeathCount => deathCount;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        currentCheckpoint = defaultRespawnPoint != null ? defaultRespawnPoint.position : Vector3.zero;
    }

    public void UpdateCheckpoint(Vector3 position)
    {
        currentCheckpoint = position;
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
        yield return new WaitForSeconds(respawnDelay);
        player.Respawn(currentCheckpoint);
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
