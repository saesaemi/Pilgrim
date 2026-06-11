using UnityEngine;

// 각 스테이지의 정보와 프리팹을 담는 ScriptableObject
// Assets/ScriptableObjects/Stages/ 폴더에 DAY별로 생성
[CreateAssetMenu(fileName = "StageData_Day00", menuName = "Pilgrim/Stage Data")]
public class StageData : ScriptableObject
{
    [Header("스테이지 정보")]
    public int DayNumber;
    public string StageTitle;
    public bool IsBuffStage;
    public bool IsGuide;
    public string StageDesc;

    [Header("프리팹")]
    public GameObject StagePrefab;  // 해당 DAY의 스테이지 프리팹
}
