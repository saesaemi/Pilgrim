using UnityEngine;

public class IntroPage : MonoBehaviour
{
    
    public void OnClickSelectStage()
    {
        UIManager.Instance.Get(UIManager.UIType.SELECTSTAGE);
    }
    public void OnlickSetting()
    {
        UIManager.Instance.Get(UIManager.UIType.SETTING);
    }
    public void OnClickExit()
    {
        Application.Quit();
    }
}
