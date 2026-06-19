using System.Collections;
using TMPro;
using UnityEngine;

public class GamePage : MonoBehaviour
{
    public TextMeshProUGUI Title;
    public TextMeshProUGUI Desc;
    public void GotoStageSelect()
    {
        StageLoader.Instance.ResetStage();
        GameManager.Instance.GotoSelectStage();
    }
    public void GotoIntroScene()
    {
        StageLoader.Instance.ResetStage();
        GameManager.Instance.GotoIntro();
    }
    public void SetText(string title, string desc)
    {
        Title.text = title;
        Desc.text = desc;
        Desc.gameObject.SetActive(true);
        StartCoroutine(ShowText());
    }
    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(2f);
        if(Desc != null)
            Desc.gameObject.SetActive(false);

    }
}
