using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class CreateDecoPanelScript : MonoBehaviour
{
    public ToDoList currentToDoListInfo = new ToDoList();
    public GameObject currentTodoListObj;
    private TMP_InputField toDoNameInputField;
    private TextMeshProUGUI startDateText;
    private TMP_Dropdown endYearDropDown;
    private TMP_Dropdown endMonthDropDown;
    private TMP_Dropdown endDayDropDown;
    private TMP_InputField memoInputfield;
    private Image decorImage;
    private string nowImage;
    private Button editButton;
    private Button cancleButton;
    private Button toBagButton;
    private Button placeButton;

    private GameObject decorSelectPanel;
    private GameObject decorListContent;

    public void SetToDoObject(GameObject gameObject)
    {
        currentTodoListObj = gameObject;
    }
    public void SetImage(string imageName)
    {
        nowImage = imageName;
        if (decorImage == null)
        {
            decorImage = transform.Find("DecoImageBG/DecoImage").GetComponent<Image>();
        }
        decorImage.sprite = Addressables.LoadAssetAsync<Sprite>(imageName).WaitForCompletion();
    }
    public void SetPanel(ToDoList selectedToDoList)
    {
        currentToDoListInfo = selectedToDoList;
        if (toDoNameInputField == null)
        {
            toDoNameInputField = transform.Find("ToDoNameInputField").GetComponent<TMP_InputField>();
            startDateText = transform.Find("StartDateText").GetComponent<TextMeshProUGUI>();
        }
        toDoNameInputField.text = currentToDoListInfo.toDoName;
        startDateText.text = currentToDoListInfo.startDate;
        transform.parent.gameObject.SetActive(true);
    }
    public void CancleButtonClicked()
    {
        transform.parent.gameObject.SetActive(false);
    }
    public void EditButtonClicked()
    {
        //DecorSelectPanelBack.SetActive(true)
        if (decorSelectPanel == null)
        {
            decorSelectPanel = GameObject.Find("Canvas").transform.Find("DecorSelectPanelBack").gameObject;
            decorListContent = decorSelectPanel.transform.Find("DecorSelectPanel/DecorListScroll/Viewport/Content").gameObject;
        }

        //스크롤의 모든 장식 정보 삭제하고 새로 넣음
        foreach (Transform child in decorListContent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        Dictionary<string, Decor> decorDict = GameManager.GetInstance().decorDict;
        foreach (string decorName in decorDict.Keys)
        {
            //프리팹 생성
            GameObject decorList = Addressables.InstantiateAsync("DecorList").WaitForCompletion();
            decorList.transform.SetParent(decorListContent.transform, false);

            //이미지 변경
            Image decorListImage = decorList.transform.Find("Image").GetComponent<Image>();
            decorListImage.sprite = Addressables.LoadAssetAsync<Sprite>(decorName).WaitForCompletion();

            //텍스트 변경
            TextMeshProUGUI decorListName = decorList.transform.Find("DecorNameText").GetComponent<TextMeshProUGUI>();
            decorListName.text = decorDict[decorName].name[SettingManager.GetInstance().currentLanguage];

            //DecorListScript의 DecorName 넣기
            DecorListScript decorListScript = decorList.GetComponent<DecorListScript>();
            decorListScript.decorName = decorName;
        }

        decorSelectPanel.SetActive(true);

    }
    public DecorItem CreateDecorItem()
    {
        if (endYearDropDown == null)
        {
            endYearDropDown = transform.Find("EndYearDropDown").GetComponent<TMP_Dropdown>();
            endMonthDropDown = transform.Find("EndMonthDropDown").GetComponent<TMP_Dropdown>();
            endDayDropDown = transform.Find("EndDayDropDown").GetComponent<TMP_Dropdown>();
            memoInputfield = transform.Find("MemoInputField").GetComponent<TMP_InputField>();
        }
        int endYear = int.Parse(endYearDropDown.options[endYearDropDown.value].text);
        int endMonth = int.Parse(endMonthDropDown.options[endMonthDropDown.value].text);
        int endDay = int.Parse(endDayDropDown.options[endDayDropDown.value].text);
        //데코 아이템 만들기
        DecorItem decorItem = new DecorItem()
        {
            name = toDoNameInputField.text,
            spriteName = nowImage,
            startDate = startDateText.text,
            endDate = $"{GameManager.GetInstance().DateParse(endYear, endMonth, endDay)}",
            memo = memoInputfield.text
        };
        return decorItem;
    }
    public void ToBagButtonClicked()
    {
        //GameManager의 PlayerInfo의 Inventory에 넣기
        GameManager.GetInstance().AddInventory(CreateDecorItem());
        //GotDecorListScroll => Viewport => Content에 GotDecorList 프리팹 생성
        ResetFields();
        Destroy(currentTodoListObj);
        transform.parent.gameObject.SetActive(false);
    }
    public void PlaceButtonClicked()
    {
        //GameManager의 Place메서드 실행
        GameManager.GetInstance().PlaceDecorItem(CreateDecorItem());
        transform.parent.gameObject.SetActive(false);
    }
    public void ResetFields()
    {
        toDoNameInputField.text = "";
        startDateText.text = "";
        endYearDropDown.value = 0;
        endMonthDropDown.value = 0;
        endDayDropDown.value = 0;
        memoInputfield.text = "";
    }
    public void DecorSelectPanelControl()
    {
        decorSelectPanel.SetActive(!decorSelectPanel.activeSelf);
    }
    void Start()
    {
        toDoNameInputField = transform.Find("ToDoNameInputField").GetComponent<TMP_InputField>();

        startDateText = transform.Find("StartDateText").GetComponent<TextMeshProUGUI>();

        editButton = transform.Find("EditButtonBG/EditButton").GetComponent<Button>();
        editButton.onClick.AddListener(EditButtonClicked);

        cancleButton = transform.Find("CancleButton").GetComponent<Button>();
        cancleButton.onClick.AddListener(CancleButtonClicked);

        toBagButton = transform.Find("ToBagButton").GetComponent<Button>();
        toBagButton.onClick.AddListener(ToBagButtonClicked);

        placeButton = transform.Find("PlaceButton").GetComponent<Button>();
        placeButton.onClick.AddListener(PlaceButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
