using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    private static SettingManager instance;
    public int currentLanguage; // 0 영어 / 1 한국어 / 2 일본어
    // Start is called before the first frame update
    void Start()
    {
        currentLanguage = 1;
    }

    // Update is called once per frame
    void Update()
    {

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
