using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    private static SettingManager instance;
    public UIManager uiManager;
    public int currentLanguage; // 0 영어 / 1 한국어 / 2 일본어
    public List<string> languages = new List<string>() { "English", "한국어", "日本語"};

    //UI
    public TextMeshProUGUI currentLanguageText;
    public Button leftButton;
    public Button rightButton;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = UIManager.GetInstance();
        currentLanguage = 1;
        leftButton.onClick.AddListener(LeftButtonClicked);
        rightButton.onClick.AddListener(RightButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LeftButtonClicked()
    {
        currentLanguage -= 1;
        if (currentLanguage < 0) currentLanguage = languages.Count - 1;
        uiManager.ChangeUILanguage(currentLanguage);
    }
    public void RightButtonClicked()
    {
        currentLanguage += 1;
        if (currentLanguage > languages.Count - 1) currentLanguage = 0;
        uiManager.ChangeUILanguage(currentLanguage);
    }

    public int GetCurrentLanguage()
    {
        return currentLanguage;
    }

    public void SetCurrentLanguage(int languageNum)
    {
        currentLanguage = languageNum;
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //싱글턴의 Instance를 가져오는 메서드
    public static SettingManager GetInstance()
    {
        if (instance == null)
        {
            GameObject settingManagerObj = new GameObject("SettingManager");
            instance = settingManagerObj.AddComponent<SettingManager>();
            DontDestroyOnLoad(settingManagerObj);
        }
        return instance;
    }
}
