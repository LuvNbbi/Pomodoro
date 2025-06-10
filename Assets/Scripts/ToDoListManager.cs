using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Unity.VisualScripting;


public class ToDoListManager : MonoBehaviour
{
    private static ToDoListManager instance;
    public UIManager uiManager;
    public GameObject ToDoContent;
    public TMP_InputField toDoNameInputField;
    public TMP_Dropdown startYearDropDown;
    public TMP_Dropdown startMonthDropDown;
    public TMP_Dropdown startDayDropDown;
    public TMP_Dropdown endYearDropDown;
    public TMP_Dropdown endMonthDropDown;
    public TMP_Dropdown endDayDropDown;
    public ToDoList toDoListToDecoInfo;
    public string toDoName;
    public int startYear;
    public int startMonth;
    public int startDay;
    public int endYear;
    public int endMonth;
    public int endDay;
    public int startRangeValue = 0;
    public int endRangeValue = 1000;

    public GameObject decorInfoPanel;
    public Image decorInfoImage;
    public TextMeshProUGUI decorInfoToDoNameTexts;
    public TextMeshProUGUI decorInfoDateText;
    public TextMeshProUGUI decorInfoMemoTexts;

    public void SetTodoListToDecoInfo(ToDoList selectedToDoList)
    {
        toDoListToDecoInfo = selectedToDoList;
        Debug.Log($"{toDoListToDecoInfo.toDoName} 설정완료");
    }

    public ToDoList GetAddInfo()
    {
        startYear = int.Parse(startYearDropDown.options[startYearDropDown.value].text);
        startMonth = int.Parse(startMonthDropDown.options[startMonthDropDown.value].text);
        startDay = int.Parse(startDayDropDown.options[startDayDropDown.value].text);
        endYear = int.Parse(endYearDropDown.options[endYearDropDown.value].text);
        endMonth = int.Parse(endMonthDropDown.options[endMonthDropDown.value].text);
        endDay = int.Parse(endDayDropDown.options[endDayDropDown.value].text);

        ToDoList toDoList = new ToDoList() { };
        toDoList.toDoName = toDoNameInputField.text;
        toDoList.startDate = GameManager.GetInstance().DateParse(startYear, startMonth, startDay);
        toDoList.endDate = GameManager.GetInstance().DateParse(endYear, endMonth, endDay);
        toDoList.startRange = 0;
        toDoList.endRange = 100;
        toDoList.nowRange = 0;
        toDoList.isComplete = false;

        Debug.Log($"ToDoName {toDoName} // startDate {startYear}.{startMonth}.{startDay} // endDate {endYear}.{endMonth}.{endDay}");

        return toDoList;
    }

    public void DelToDoList()
    {

    }

    public void SetDecorInfoPanel(DecorItem selectDecor)
    {
        if (decorInfoImage == null)
        {
            Debug.Log($"decorInfoImage가 null입니다.");
        }
        decorInfoImage.sprite = Addressables.LoadAssetAsync<Sprite>(selectDecor.spriteName).WaitForCompletion();
        decorInfoToDoNameTexts.text = selectDecor.name;
        decorInfoDateText.text = selectDecor.startDate + " ~ " + selectDecor.endDate;
        decorInfoMemoTexts.text = selectDecor.memo;
        DecorInfoPanelControl();
    }
    public void DecorInfoPanelControl()
    {
        decorInfoPanel.SetActive(!decorInfoPanel.activeSelf);
    }
    public void CreateToDoListObject(ToDoList changeToDoList)
    {
        //프리팹으로 오브젝트 생성
        GameObject toDoList = Addressables.InstantiateAsync("ToDoList").WaitForCompletion();

        //스크립트 가져오기
        ToDoListScript script = toDoList.GetComponent<ToDoListScript>();
        script.toDoListInfo.nowRange = 0;
        script.toDoListInfo.isComplete = false;

        //ToDoNameText
        TextMeshProUGUI toDoNameText = toDoList.transform.Find("ToDoNameText").GetComponent<TextMeshProUGUI>();
        toDoNameText.text = changeToDoList.toDoName;
        script.toDoListInfo.toDoName = changeToDoList.toDoName;
        toDoNameInputField.text = "";

        //StartDateTExt
        TextMeshProUGUI startDateText = toDoList.transform.Find("StartDateText").GetComponent<TextMeshProUGUI>();
        startDateText.text = changeToDoList.startDate;
        script.toDoListInfo.startDate = changeToDoList.startDate;
        startYearDropDown.value = 0;
        startMonthDropDown.value = 0;
        startDayDropDown.value = 0;

        //EndDateText
        TextMeshProUGUI endDateText = toDoList.transform.Find("EndDateText").GetComponent<TextMeshProUGUI>();
        endDateText.text = changeToDoList.endDate;
        script.toDoListInfo.endDate = changeToDoList.endDate;
        endYearDropDown.value = 0;
        endMonthDropDown.value = 0;
        endDayDropDown.value = 0;
        //StartRange
        TextMeshProUGUI startRange = toDoList.transform.Find("StartRange").GetComponent<TextMeshProUGUI>();
        script.toDoListInfo.startRange = changeToDoList.startRange;
        startRange.text = changeToDoList.startRange.ToString();
        //EndRange
        TextMeshProUGUI endRange = toDoList.transform.Find("EndRange").GetComponent<TextMeshProUGUI>();
        script.toDoListInfo.endRange = changeToDoList.endRange;
        endRange.text = changeToDoList.endRange.ToString();
        //RangeSlider
        Slider rangeSlider = toDoList.transform.Find("RangeSlider").GetComponent<Slider>();
        rangeSlider.minValue = changeToDoList.startRange;
        rangeSlider.maxValue = changeToDoList.endRange;

        //목표 content에 추가
        toDoList.transform.SetParent(ToDoContent.transform, false);
    }
    public void AddToDoButtonClicked()
    {
        if (GameManager.GetInstance().playerInfo.toDoLists.ContainsKey(toDoNameInputField.text))
        {
            Debug.Log("같은 이름의 목표가 있습니다");
            return;
        }
        ToDoList to = GetAddInfo();
        //입력된 정보를 가져옴
        CreateToDoListObject(to);

        //플레이어 정보 Json에 추가
        GameManager.GetInstance().SaveToDoList(to);

        //패널 닫음
        uiManager.AddToDoListPanelControl();
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GetAddInfo();
        }
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
    public static ToDoListManager GetInstance()
    {
        if (instance == null)
        {
            GameObject obj = new GameObject("ToDoListManager");
            instance = obj.AddComponent<ToDoListManager>();
            DontDestroyOnLoad(obj);
        }
        return instance;
    }
}
