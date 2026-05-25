using UnityEngine;

// 각 스테이지의 정보와 프리팹을 담는 ScriptableObject
// Assets/ScriptableObjects/Stages/ 폴더에 DAY별로 생성
[CreateAssetMenu(fileName = "StageData_Day00", menuName = "Pilgrim/Stage Data")]
public class StageData : ScriptableObject
{
    [Header("스테이지 정보")]
    public int dayNumber;
    public string stageTitle;
    public bool isBuffStage;

    [Header("프리팹")]
    public GameObject stagePrefab;  // 해당 DAY의 스테이지 프리팹
}
