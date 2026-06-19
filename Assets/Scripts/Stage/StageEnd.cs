using UnityEngine;

public class StageEnd : MonoBehaviour
{
  
    public void GotoIntroScene()
    {
        StageLoader.Instance.ResetStage();
        GameManager.Instance.GotoIntro();
    }
}
