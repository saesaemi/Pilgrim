using UnityEngine;

// 40개 스테이지 데이터를 순서대로 보관하는 ScriptableObject
// Assets/ScriptableObjects/ 에 하나만 생성해서 StageLoader에 연결
[CreateAssetMenu(fileName = "StageDatabase", menuName = "Pilgrim/Stage Database")]
public class StageDatabase : ScriptableObject
{
    [SerializeField] private StageData[] stages;

    public int TotalStages => stages.Length;

    public StageData Get(int index)
    {
        if (index < 0 || index >= stages.Length)
        {
            Debug.LogError($"StageDatabase: 인덱스 {index} 범위 초과 (총 {stages.Length}개)");
            return null;
        }
        return stages[index];
    }

    // 에디터에서 stages 배열을 채우기 쉽도록 검증
    private void OnValidate()
    {
        for (int i = 0; i < stages.Length; i++)
        {
            if (stages[i] == null)
                Debug.LogWarning($"StageDatabase: [{i}] 비어 있음");
        }
    }
}
