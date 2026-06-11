using System;
using TMPro;
using UnityEngine;

public class List_Stage : MonoBehaviour
{
    public TextMeshProUGUI Tmp;
    private int index;
    private Action<int> onClick;
        
    public void Setup(int index, Action<int> OnClick)
    {
        this.index = index;
        onClick = OnClick;
        Tmp.text = (index +1).ToString();
    }
    public void OnClickStage()
    {
        onClick(index);
    }
}
