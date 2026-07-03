using System.Collections.Generic;
using UnityEngine;

public class StageSelectPage : MonoBehaviour
{
    public GameObject Prefab;

    private List<List_Stage> list = new List<List_Stage>();
    private void Awake()
    {
        Prefab.SetActive(false);
        var stageCount = StageLoader.Instance.StageCount - 1;
        for (int i = 0; i < stageCount; i++) 
        { 
            var go = Instantiate(Prefab, Prefab.transform.parent);
            go.SetActive(true);
            var stage = go.GetComponent<List_Stage>();
            stage.Setup(i, OnClickStage);
            list.Add(stage);
        }
    }
    public void OnClickStage(int index)
    {
        UIManager.Instance.Get(UIManager.UIType.GAME);
        StageLoader.Instance.LoadStage(index);
    }

    public void OnClickIntro()
    {
        UIManager.Instance.Get(UIManager.UIType.INTRO);
    }
}
