using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    private static SettingManager instance;
    public UIManager uiManager;
    public int currentLanguage; // en 영어 / ko 한국어 / jp 일본어

    public List<string> languageCodes = new List<string>() { "en", "ko", "ja" };

    //UI
    public TextMeshProUGUI currentLanguageText;
    public Button leftButton;
    public Button rightButton;

    // Start is called before the first frame update
    void Start()
    {
        uiManager = UIManager.GetInstance();
        currentLanguage = GameManager.GetInstance().playerInfo.userSetting.language;
        var locale = LocalizationSettings.AvailableLocales.GetLocale(languageCodes[currentLanguage]);
        LocalizationSettings.SelectedLocale = locale;
        leftButton.onClick.AddListener(LeftButtonClicked);
        rightButton.onClick.AddListener(RightButtonClicked);
    }
    public void LeftButtonClicked()
    {
        Debug.Log("언어 변경 버튼 클릭");
        currentLanguage -= 1;
        if (currentLanguage < 0) currentLanguage = languageCodes.Count - 1;
        GameManager.GetInstance().playerInfo.userSetting.language = currentLanguage;
        GameManager.GetInstance().SavePlayerInfo();
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(languageCodes[currentLanguage]);
    }
    public void RightButtonClicked()
    {
        Debug.Log("언어 변경 버튼 클릭");
        currentLanguage += 1;
        if (currentLanguage > languageCodes.Count - 1) currentLanguage = 0;
        GameManager.GetInstance().playerInfo.userSetting.language = currentLanguage;
        GameManager.GetInstance().SavePlayerInfo();
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(languageCodes[currentLanguage]);
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
