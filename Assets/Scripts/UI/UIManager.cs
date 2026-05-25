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

    private GameObject current = null;
    private void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this);
    }
    public void Get(UIType type)
    {
        if (current != null)
        {
            Destroy(current);
            current = null;
        }
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
}
