using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    UIManager uiManager;
    void Start()
    {
        uiManager = UIManager.GetInstance();
        //게임 시작 시 정보 불러오기
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
