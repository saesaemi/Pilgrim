using Unity.Entities;
using UnityEngine;

public class UIManager : MonoBehaviour
{  
    public enum UIType
    {
        INTRO,
        SELECTSTAGE,
        SETTING,
        GAME
    }
    private static UIManager _instance;
    public static UIManager Instance => _instance;
    public GameObject Stack;
    public GameObject IntroPrefab;
    public GameObject SelectStagePrefab;
    public GameObject SettingPrefab;
    public GameObject GamePrefab;
    public GameObject GuidePrefab;

    private GameObject current = null;
    private GameObject guide = null;
    private UIType currentType = UIType.INTRO;
    public UIType CurrentType => currentType;
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }
    public void Get(UIType type)
    {
        if (current != null && currentType != type)
        {
            Destroy(current);
            current = null;
        }
        currentType = type;
        GameObject prefab = null;
        switch (type)
        {
            case UIType.INTRO:
                prefab = IntroPrefab;
                break;
            case UIType.SELECTSTAGE:
                prefab = SelectStagePrefab;
                break;
            case UIType.SETTING:
                prefab = SettingPrefab;
                break;
            case UIType.GAME:
                prefab = GamePrefab;
                break;
        }
        if(prefab != null)
        {
            current = Instantiate(prefab, Stack.transform);
        }

    }
    public void SetGuide()
    {
        RemoveGuide();
        guide = Instantiate(GuidePrefab, Stack.transform);
    }
    public void RemoveGuide()
    {
        if(guide != null)
        {
            Destroy(guide);
            guide = null;
        }
    }
}
