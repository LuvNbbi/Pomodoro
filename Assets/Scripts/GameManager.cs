using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Newtonsoft.Json;
using System.IO;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public PlayerInfo playerInfo;
    public Dictionary<string, Decor> decorDict;
    UIManager uiManager;
    public FollowMouse followMouse;
    public CreateDecoPanelScript createDecoPanelScript;
    private Camera mainCamera;
    public Vector2 mousePos;
    public bool isPlaceMode;
    public DecorItem placeDecorItem = new DecorItem();
    public void EndPlaceMode()
    {
        isPlaceMode = false;
        followMouse.HideFollowImage();
        createDecoPanelScript.ResetFields();
        Destroy(createDecoPanelScript.currentTodoListObj);
    }
    public void PlaceDecorItem(DecorItem decorItem)
    {
        placeDecorItem = decorItem;
        isPlaceMode = true;
        followMouse.ShowFollowImage(placeDecorItem.spriteName);
    }
    public void SaveToDoList(ToDoList toDoList)
    {
        playerInfo.toDoLists.Add(toDoList.toDoName, toDoList);
        SavePlayerInfo();
    }
    private T JsonLoad<T>(string jsonName)
    {
        // 파일 경로 만들기
        string filePath = Path.Combine(Application.persistentDataPath, jsonName + ".json");
        Debug.Log(filePath);

        if (!File.Exists(filePath))
        {
            Debug.LogWarning($"파일이 존재하지 않습니다. : {filePath}");
            if (jsonName == "PlayerInfo")
            {
                PlayerInfo playerInfo = new PlayerInfo()
                {
                    money = 0,
                    level = 1,
                    exp = 0,
                    toDoLists = new Dictionary<string, ToDoList>() { },
                    furnitures = new Dictionary<string, PlacedFurnitureInfo>()
                    {
                        {"BookShelf_001",new PlacedFurnitureInfo(){
                            prefabName = "BookShelf_001",
                            x = -195f,
                            y = -56f,
                            placedItems = new Dictionary<string, DecorItem>()
                            {
                                {"0", new DecorItem()
                                {
                                    name = "테스트용",
                                    spriteName = "FlowerVase",
                                    startDate = "2025.06.04",
                                    endDate = "2025.06.16",
                                    memo = "흑흑"
                                }
                                }
                            }
                        }
                        },
                        {"Window",new PlacedFurnitureInfo(){
                            prefabName = "Window",
                            x = 0f,
                            y = 5f,

                        }
                        }

                    },
                    decorInventory = new List<DecorItem>(),
                    decor = new Dictionary<string, bool>()
                    {
                        {"TeddyBear", false},
                        {"FlowerVase", true},
                        {"Books", false}
                    },
                    userSetting = new UserSetting()
                    {
                        sound = 50,
                        language = 1, //한국어
                    }
                };
                JsonSave<PlayerInfo>(playerInfo, "PlayerInfo");
            }
        }
        string jsonText = File.ReadAllText(filePath);
        T data = JsonConvert.DeserializeObject<T>(jsonText);
        return data;
    }
    public void SavePlayerInfo()
    {
        JsonSave<PlayerInfo>(playerInfo, "PlayerInfo");
    }
    private void JsonSave<T>(T data, string jsonName)
    {
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);
        string path = Path.Combine(Application.persistentDataPath, jsonName + ".json");
        File.WriteAllText(path, json);
    }
    public T GetDictionaryJson<T>(string dictionaryName)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>($"Json/Dictionary/{dictionaryName}");
        if (jsonFile != null)
        {
            return JsonConvert.DeserializeObject<T>(jsonFile.text);
        }

        Debug.LogWarning($"Json 파일을 찾지 못했습니다: {dictionaryName}");
        return default(T); // 제네릭 null 대응 안전하게
    }
    public void AddInventory(DecorItem decorItem)
    {
        playerInfo.decorInventory.Add(decorItem);
        JsonSave<PlayerInfo>(playerInfo, "PlayerInfo");
    }
    public string DateParse(int year, int month, int day)
    {
        string str = year + ".";
        if (month / 10 > 0)
        {
            str += "0";
        }
        str += month + ".";
        if (day / 10 > 0)
        {
            str += "0";
        }
        str += day;
        return str;
    }
    public void LoadPlaceFurniture()
    {
        if (playerInfo.furnitures.Count <= 0) return;
    }
    void Start()
    {
        uiManager = UIManager.GetInstance();
        //게임 시작 시 정보 불러오기
        playerInfo = JsonLoad<PlayerInfo>("PlayerInfo");
        decorDict = GetDictionaryJson<Dictionary<string, Decor>>("DecorDictionary");
        isPlaceMode = false;
        mainCamera = Camera.main;

        List<ToDoList> savedToDoList = new List<ToDoList>() { };
        foreach (string key in playerInfo.toDoLists.Keys)
        {
            savedToDoList.Add(playerInfo.toDoLists[key]);
        }

        //불러온 playerInfo로 목표 배치
        foreach (ToDoList info in savedToDoList)
        {
            ToDoListManager.GetInstance().CreateToDoListObject(info);
        }

        List<PlacedFurnitureInfo> placedFurnitures = new List<PlacedFurnitureInfo>();
        foreach (string key in playerInfo.furnitures.Keys)
        {
            placedFurnitures.Add(playerInfo.furnitures[key]);
        }

        //불러온 PlayerInfo로 가구 배치
        foreach (PlacedFurnitureInfo info in placedFurnitures)
        {
            //일단 프리팹 생성
            GameObject furniture = Addressables.InstantiateAsync(info.prefabName).WaitForCompletion();
            furniture.name = furniture.name.Split("(")[0];

            //Canvas 밑의 BackGround의 자식으로 만듬
            furniture.transform.SetParent(GameObject.Find("Canvas/BackGround").transform, false);

            //위치를 변경함
            RectTransform rect = furniture.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(info.x, info.y);

            //자식 위치 변경
            furniture.transform.SetSiblingIndex(info.siblingIndex);

            //가구가 가지고 있는 모든 PlacePoint를 모음 (모든 자식을 가져온 뒤 맨 첫번째꺼를 제외하고 나머지가 PlacePoint)
            List<GameObject> placePoints = new List<GameObject>();
            foreach (Transform child in furniture.transform)
            {
                placePoints.Add(child.gameObject);
            }
            if (placePoints.Count > 0)
            {
                placePoints.RemoveAt(0);
            }
            //placedItems의 숫자만큼 가구 배치 Keys는 PlacePoint의 위치
            if (info.placedItems != null)
            {
                Dictionary<string, DecorItem> dict = info.placedItems;
                foreach (string key in dict.Keys)
                {
                    GameObject selectPlacePoint = placePoints[int.Parse(key)];
                    DecorItem selectDecorItem = dict[key];

                    PlacePointScript script = selectPlacePoint.GetComponent<PlacePointScript>();
                    script.SetPlaceItemInfo(selectDecorItem);
                }
            }
        }

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            uiManager.SettingPanelControl();
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
    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            GameObject gameManagerObj = new GameObject("GameManager");
            instance = gameManagerObj.AddComponent<GameManager>();
            DontDestroyOnLoad(gameManagerObj);
        }
        return instance;
    }
}
