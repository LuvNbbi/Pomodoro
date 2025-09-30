using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [Header("배경")]
    public GameObject backgroundUI;
    [Header("스크롤 뷰")]
    public HairScrollView hairScrollView;
    public ClothesScrollView clothesScrollView;
    public DecorImageListScrollScript decorScrollView;
    public GotDecorListScrollScript gotDecorListScrollScript;
    public GotDecorListScript placeGotDecorList;
    public PlayerInfo playerInfo;
    public Dictionary<string, Decor> decorDict;
    UIManager uiManager;
    public FollowMouse followMouse;
    public CreateDecoPanelScript createDecoPanelScript;
    private Camera mainCamera;
    public Vector2 mousePos;
    public bool isPlaceMode;
    public bool isPlaceToBag;
    public DecorItem placeDecorItem = new DecorItem();
    public string currentHair;
    public string currentClothes;
    public string currentBackground;
    public PomodoroManager pomodoroManager;

    //캐릭터
    public Image hairObj;
    public Image clothesObj;
    public Image bodyObj;

    public Dictionary<string, Decor> GetDecorDict()
    {
        return decorDict;
    }
    public void RemoveToDoList(string ToDoName)
    {
        playerInfo.toDoLists.Remove(ToDoName);
        JsonSave(playerInfo, "PlayerInfo");
    }
    public void RemovePlacedDecor(DecorItem decorItem, string objName)
    {
        //정보에서 삭제
        playerInfo.backgrounds[playerInfo.backGround][objName] = new() { item = null, isPlaced = false };

        //인벤토리에 추가
        AddInventory(decorItem);
        gotDecorListScrollScript.RefreshDecorList();
        //
        SetPlacePoint(backgroundUI.transform.Find("currentBG").gameObject);

        backgroundUI.transform.Find("currentBG").gameObject.transform.Find(objName).GetComponent<PlacePointScript>().DeletePlaceItem();
        //정보 저장
        JsonSave<PlayerInfo>(playerInfo, "PlayerInfo");
    }
    public void RemoveGotDecorList(int index)
    {
        playerInfo.decorInventory.RemoveAt(index);
        SavePlayerInfo();
        gotDecorListScrollScript.RefreshDecorList();
    }
    public void IncreaseGameMoney(int money)
    {
        playerInfo.money += money;
        SavePlayerInfo();
    }
    public void DecreaseGameMoney(int money)
    {
        playerInfo.money -= money;
        SavePlayerInfo();
    }
    public List<DecorItem> GetDecorInventory()
    {
        return playerInfo.decorInventory;
    }
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
    public void AddGotBG(string bgNum)
    {
        playerInfo.gotBG.Add(bgNum);
    }
    public T JsonLoad<T>(string jsonName)
    {
        // 파일 경로 만들기
        string filePath = Path.Combine(Application.persistentDataPath, jsonName + ".json");

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
                    hair = "Hair_000",
                    clothes = "Clothes_000",
                    backGround = "Background_000",
                    gotClothes = new List<int>() { 0 },
                    gotHairs = new List<int>() { 0 },
                    gotDecors = new() { 0 },
                    gotBG = new() { "000", "001" },
                    toDoLists = new Dictionary<string, ToDoList>() { },
                    backgrounds = new()
                    {
                        {"Background_000", new()
                        {
                            {"PlacePoint_0", new()
                            {
                                item = new(){
                                    name = "1234",
                                    spriteName = "Decor_000",
                                    startDate = "2025.09.26",
                                    endDate = "2025.09.30",
                                    memo = "21344",
                                },
                                isPlaced = true,
                            }},
                            {"PlacePoint_1", new()
                            {
                                item = null,
                                isPlaced = false,
                            }},
                            {"PlacePoint_2", new()
                            {
                                item = null,
                                isPlaced = false,
                            }},
                            {"PlacePoint_3", new()
                            {
                                item = null,
                                isPlaced = false,
                            }},
                        }},
                        {"Background_001", new()
                        {
                             {"PlacePoint_0", new()
                            {
                                item = new(){
                                    name = "312312",
                                    spriteName = "Decor_002",
                                    startDate = "2025.09.26",
                                    endDate = "2025.09.30",
                                    memo = "321312",
                                },
                                isPlaced = true,
                            }},
                            {"PlacePoint_1", new()
                            {
                                item = null,
                                isPlaced = false,
                            }},
                            {"PlacePoint_2", new()
                            {
                                item = null,
                                isPlaced = false,
                            }},
                            {"PlacePoint_3", new()
                            {
                                item = null,
                                isPlaced = false,
                            }},
                        }},
                    },
                    furnitures = new Dictionary<string, PlacedFurnitureInfo>()
                    {
                        {"Shelf",new PlacedFurnitureInfo(){
                            prefabName = "Shelf",
                            x = -660f,
                            y = -250f,
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
                        {"Shelf_2",new PlacedFurnitureInfo(){
                            prefabName = "Shelf",
                            x = 700f,
                            y = -130f,
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
        return year + "." + month.ToString("D2") + "." + day.ToString("D2");
    }
    public void LoadPlaceFurniture()
    {
        if (playerInfo.furnitures.Count <= 0) return;
    }
    public void chgCharacterInfo(string hair, string clothes, string backGround)
    {
        playerInfo.hair = hair;
        playerInfo.clothes = clothes;
        playerInfo.backGround = backGround;
        SavePlayerInfo();
    }
    public void chgHair(string selectHair)
    {
        //게임오브젝트 이미지 가져와서 어드레서블로 변경하기
        hairObj.sprite = Addressables.LoadAssetAsync<Sprite>(selectHair).WaitForCompletion();
        playerInfo.hair = selectHair;
        SavePlayerInfo();
    }
    public void chgClothes(string selectClothes)
    {
        //게임오브젝트 이미지 가져와서 어드레서블로 변경하기
        clothesObj.sprite = Addressables.LoadAssetAsync<Sprite>(selectClothes).WaitForCompletion();
        playerInfo.clothes = selectClothes;
        SavePlayerInfo();
    }
    void Start()
    {
        uiManager = UIManager.GetInstance();
        //게임 시작 시 정보 불러오기
        playerInfo = JsonLoad<PlayerInfo>("PlayerInfo");
        decorDict = GetDictionaryJson<Dictionary<string, Decor>>("DecorDictionary");
        isPlaceMode = false;
        isPlaceToBag = false;
        mainCamera = Camera.main;

        chgCharacterInfo(playerInfo.hair, playerInfo.clothes, playerInfo.backGround);

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


        //Json의 backGround를 보고 프리팹 불러옴
        GameObject startBG = Addressables.InstantiateAsync(playerInfo.backGround).WaitForCompletion();
        startBG.name = "currentBG";
        startBG.transform.SetParent(backgroundUI.transform, false);

        //Json의 backgrounds를 보고 배경에 저장된 장식을 불러옴
        SetPlacePoint(startBG);
        //헤어랑 옷 변경
        //hairObj.sprite = Addressables.LoadAssetAsync<Sprite>(playerInfo.hair).WaitForCompletion();
        //clothesObj.sprite = Addressables.LoadAssetAsync<Sprite>(playerInfo.clothes).WaitForCompletion();

        //장식 인벤토리 초기화
        gotDecorListScrollScript.RefreshDecorList();

        //돈 UI 초기화
        UIManager.GetInstance().RefreshMoney();

        //헤어 리스트
        hairScrollView.SetHairList();

        //옷 리스트
        clothesScrollView.SetClothesList();

        //장식 리스트
        decorScrollView.SetDecorList();

    }
    public void CharacterListClose()
    {
        hairScrollView.gameObject.transform.parent.parent.gameObject.SetActive(false);
        clothesScrollView.gameObject.transform.parent.parent.gameObject.SetActive(false);
    }
    public void pomoPanelMoveConst()
    {
        pomodoroManager.isAnim = !pomodoroManager.isAnim;
    }
    public void ExitBtnClicked()
    {
        Application.Quit();
    }
    public void ChgBackground(string bgName)
    {
        playerInfo.backGround = bgName;
        foreach (Transform child in backgroundUI.transform)
        {
            Destroy(child.gameObject);
        }
        GameObject newBG = Addressables.InstantiateAsync(bgName).WaitForCompletion();
        newBG.name = "currentBG";
        newBG.transform.SetParent(backgroundUI.transform, false);
        SetPlacePoint(newBG);
        UIManager.GetInstance().MapPanelControl();
        SavePlayerInfo();
    }
    private void SetPlacePoint(GameObject currentBG)
    {
        Dictionary<string, PlacePoint> placePoints = playerInfo.backgrounds[playerInfo.backGround];
        foreach (string key in placePoints.Keys)
        {
            if (!placePoints[key].isPlaced) continue;
            currentBG.transform.Find(key).GetComponent<PlacePointScript>().SetPlaceItemInfo(placePoints[key].item);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            playerInfo.money += 1000;
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
