using System.Collections.Generic;
using UnityEngine;

public class StageSelectPage : MonoBehaviour
{
    public int StageCount = 40;
    public GameObject Prefab;

    private List<List_Stage> list = new List<List_Stage>();
    private void Awake()
    {
        Prefab.SetActive(false);
        for (int i = 0; i < StageCount; i++) 
        { 
            var go = Instantiate(Prefab, Prefab.transform.parent);
            go.SetActive(true);
            var stage = go.GetComponent<List_Stage>();
            stage.Setup(i);
            list.Add(stage);
        }
    }
    public void OnClickStage()
    {
        UIManager.Instance.Get(UIManager.UIType.GAME);
    }
}
