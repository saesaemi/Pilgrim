using System;
using TMPro;
using UnityEngine;

public class List_Stage : MonoBehaviour
{
    public TextMeshProUGUI Tmp;
    public GameObject LockGo;
    private int index;
    private Action<int> onClick;
        
    public void Setup(int index, Action<int> OnClick)
    {
        this.index = index;
        onClick = OnClick;
        Tmp.text = (index +1).ToString();
        LockGo.SetActive(GameManager.Instance.SaveStageData < index);
    }
    public void OnClickStage()
    {
        if (GameManager.Instance.SaveStageData < index)
            return;
        onClick(index);
    }
}
