using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public GameObject SettingPanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SettingPanelControl()
    {
        SettingPanel.SetActive(!SettingPanel.activeSelf);
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