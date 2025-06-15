using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public SettingManager settingManager;
    public UILanguageManager uiLanguageManager;

    //UI들
    //SettingPanel Text들
    public TextMeshProUGUI settingPanelText;
    public TextMeshProUGUI languagePanelText;
    public TextMeshProUGUI currentLanguageText;
    public TextMeshProUGUI soundPanelText;
    public TextMeshProUGUI exitButtonText;
    public TextMeshProUGUI closeButtonText;

    //Pomodoro Text들
    public TextMeshProUGUI focusText;
    public TextMeshProUGUI setTimeText;
    public TextMeshProUGUI playButtonText;
    public TextMeshProUGUI stopButtonText;
    public TextMeshProUGUI resetButtonText;
    public GameObject SettingPanel;
    public TextMeshProUGUI focusTimeSetText;
    public TextMeshProUGUI breakTimeSetText;
    public TextMeshProUGUI focusTimeApplyText;
    public TextMeshProUGUI breakTimeApplyText;
    public TextMeshProUGUI loopButtonText;

    //목표 Texts   
    public TextMeshProUGUI toDoListPanelName;
    //목표 추가 UI
    public GameObject AddToDoListPanel;
    //목표 추가하기 Text
    public TextMeshProUGUI toDoListAddPanelName;
    public TextMeshProUGUI toDoNameText;
    public TextMeshProUGUI toDoNameInputFieldPlace;
    public TextMeshProUGUI toDoNameInputFieldText;
    public TextMeshProUGUI startDayText;
    public TextMeshProUGUI endDayText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI backButtonText;
    public TextMeshProUGUI addButtonText;

    //가방 UI
    public GameObject bagPanel;

    //게임 머니 UI
    public TextMeshProUGUI moneyUI;

    //상점 ui
    public GameObject shopPanel;
    public GameObject characterStylePanel;


    public Dictionary<string, TextMeshProUGUI> textUINames;
    public List<TMP_FontAsset> fontNames = new List<TMP_FontAsset>();

    public List<StyleItem> clothesItems = new List<StyleItem>()
    {
        new StyleItem(){
            type = "Clothes",
            name = "Clothes_000",
            spriteName = "Clothes_000"
        },
        new StyleItem(){
            type = "Clothes",
            name = "Clothes_001",
            spriteName = "Clothes_001"
        }
    };
    // Start is called before the first frame update
    void Start()
    {
        textUINames = new Dictionary<string, TextMeshProUGUI>()
    {
        {"SettingPanelText", settingPanelText},
        {"LanguagePanelText", languagePanelText},
        {"SoundPanelText", soundPanelText},
        {"ExitButtonText", exitButtonText},
        {"CloseButtonText", closeButtonText},
        {"FocusText", focusText},
        {"SetTimeText", setTimeText},
        {"PlayButtonText", playButtonText},
        {"StopButtonText", stopButtonText},
        {"ResetButtonText", resetButtonText},
        {"CurrentLanguageText", currentLanguageText},
        {"FocusTimeSetText", focusTimeSetText},
        {"BreakTimeSetText", breakTimeSetText},
        {"FocusTimeApplyText", focusTimeApplyText},
        {"BreakTimeApplyText", breakTimeApplyText},
        {"LoopButtonText", loopButtonText},
        {"ToDoListPanelName", toDoListPanelName},
        {"ToDoNameText", toDoNameText},
        {"ToDoNameInputFieldPlace", toDoNameInputFieldPlace},
        {"ToDoNameInputFieldText", toDoNameInputFieldText},
        {"StartDayText", startDayText},
        {"EndDayText", endDayText},
        {"RangeText", rangeText},
        {"BackButtonText", backButtonText},
        {"AddButtonText", addButtonText},
    };

    }


    // Update is called once per frame
    void Update()
    {

    }
    public void CharacterStylePanelControl()
    {
        characterStylePanel.SetActive(!characterStylePanel.activeSelf);
    }
    public void ShopPanelControl()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }
    public void RefreshMoney()
    {
        moneyUI.text = GameManager.GetInstance().playerInfo.money.ToString();
    }

    public void BagPanelControl()
    {
        bagPanel.SetActive(!bagPanel.activeSelf);
    }
    public void ChangeUILanguage(int currentLanguage)
    {
        foreach (string key in textUINames.Keys)
        {
            textUINames[key].text = uiLanguageManager.GetUILanguage(key)[currentLanguage];
            textUINames[key].font = fontNames[currentLanguage];
        }
    }

    public void SettingPanelControl()
    {
        SettingPanel.SetActive(!SettingPanel.activeSelf);
    }

    public void AddToDoListPanelControl()
    {
        AddToDoListPanel.SetActive(!AddToDoListPanel.activeSelf);
    }
    //싱글턴을 위한 Awake메서드
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
    public static UIManager GetInstance()
    {
        if (instance == null)
        {
            GameObject uiManagerObj = new GameObject("UIManager");
            instance = uiManagerObj.AddComponent<UIManager>();
            DontDestroyOnLoad(uiManagerObj);
        }
        return instance;
    }
}