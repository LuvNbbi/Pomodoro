using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PomodoroManager : MonoBehaviour
{
    //게임 오브젝트들
    public TextMeshProUGUI currentTimeText;
    public TextMeshProUGUI setTimeText;
    public TextMeshProUGUI focusText;
    public GameObject stopButtonPanel;
    public GameObject playButton;
    public GameObject pomodoroSettingPanel;
    public AudioSource effectAudioSource;

    //세팅 패널 관련
    public GameObject focusInputField;
    public GameObject breakInputField;
    public Image loopButtonImage;

    //언어들
    public List<string> focusTexts = new List<string>() { "Focus", "집중", "集中" };
    public List<string> breakTexts = new List<string>() { "Break", "휴식", "休息" };
    public List<string> stopTexts = new List<string>() {"Stop","일시정지", "止める" };
    public List<string> restartTexts = new List<string>() {"Restart", "재시작", "リスタート"};

    //변수들
    public int setFocusTime;
    public int setBreakTime;
    public float currentTime;
    public bool isPlay;
    public bool isLoop;
    public bool isFocus;

    //
    public SettingManager settingManager;
    public GameManager gameManager;

    void Start()
    {
        settingManager = SettingManager.GetInstance();
        gameManager = GameManager.GetInstance();
        setFocusTime = 10;
        setBreakTime = 5;
        settingManager.SetCurrentLanguage(1);
        TMP_InputField focusMinute = focusInputField.transform.Find("MinuteInput").GetComponent<TMP_InputField>();
        TMP_InputField focusSecond = focusInputField.transform.Find("SecondInput").GetComponent<TMP_InputField>();
        focusMinute.text = "00";
        focusSecond.text = "00";
        TMP_InputField breakMinute = breakInputField.transform.Find("MinuteInput").GetComponent<TMP_InputField>();
        TMP_InputField breakSecond = breakInputField.transform.Find("SecondInput").GetComponent<TMP_InputField>();
        breakMinute.text = "00";
        breakSecond.text = "00";
        isPlay = false;
        isLoop = false;
        isFocus = false;
        //setTimeText 초기설정
        ChangeSetTimeText();
    }
    void ChangeSetTimeText()
    {
        setTimeText.text = $"{ParseTime(setFocusTime)}\n{ParseTime(setBreakTime)}";
    }

    string ParseTime(int second)
    {
        string minute = (second / 60).ToString();
        string sec = (second % 60).ToString();
        if (int.Parse(minute) / 10 <= 0)
        {
            minute = "0" + minute;
        }
        if (int.Parse(sec) / 10 <= 0)
        {
            sec = "0" + sec;
        }
        return $"{minute} : {sec}";
    }

    void SetCurrentTimeText()
    {
        currentTimeText.text = ParseTime((int)currentTime);
    }


    void Update()
    {
        if (isPlay)
        {
            currentTime += Time.deltaTime;
            SetCurrentTimeText();

            //집중 시간이 지났다면 소리가 나고 휴식으로 바뀜
            if (isFocus)
            {
                if (currentTime > setFocusTime)
                {
                    //소리가 나고 isFocus = false
                    effectAudioSource.Play();
                    isFocus = false;
                    focusText.text = breakTexts[settingManager.GetCurrentLanguage()];
                    currentTime = 0;
                    SetCurrentTimeText();

                    int increaseMoney = 0;
                    //1분마다 게임 머니 10 1분 이하일 경우 X
                    if (setFocusTime >= 60)
                    {
                        increaseMoney = 10 * (setFocusTime / 60);
                    }

                    //게임 머니를 얻음
                    GameManager.GetInstance().IncreaseGameMoney(increaseMoney);
                    //게임 머니 UI 새로고침
                    UIManager.GetInstance().RefreshMoney();
                }
            }
            //집중이 아니라 휴식 시간이라면
            else
            {
                if (currentTime > setBreakTime)
                {
                    //소릭 나고 isFocus = true
                    effectAudioSource.Play();
                    isFocus = true;
                    focusText.text = focusTexts[settingManager.GetCurrentLanguage()];
                    currentTime = 0;
                    SetCurrentTimeText();
                }
            }
        }
    }
    public void FocusTimeIncreaseButton()
    {
        TMP_InputField focusSecond = focusInputField.transform.Find("SecondInput").GetComponent<TMP_InputField>();
        string secondText = focusSecond.text;
        int seconds = 0;
        if (!string.IsNullOrEmpty(secondText) && int.TryParse(secondText, out seconds) == false)
        {
            Debug.LogError("Invalid second value: " + secondText);
            return;
        }
        seconds += 5;
        focusSecond.text = seconds.ToString();
    }
    public void FocusTimeDecreaseButton()
    {
        TMP_InputField focusSecond = focusInputField.transform.Find("SecondInput").GetComponent<TMP_InputField>();
        string secondText = focusSecond.text;
        int seconds = 0;
        if (!string.IsNullOrEmpty(secondText) && int.TryParse(secondText, out seconds) == false)
        {
            Debug.LogError("Invalid second value: " + secondText);
            return;
        }
        seconds -= 5;
        focusSecond.text = seconds.ToString();
    }
    public void BreakTimeIncreaseButton()
    {
        TMP_InputField breakSecond = breakInputField.transform.Find("SecondInput").GetComponent<TMP_InputField>();
        string secondText = breakSecond.text;
        int seconds = 0;
        if (!string.IsNullOrEmpty(secondText) && int.TryParse(secondText, out seconds) == false)
        {
            Debug.LogError("Invalid second value: " + secondText);
            return;
        }
        seconds += 5;
        breakSecond.text = seconds.ToString();
    }
    public void BreakTimeDecreaseButton()
    {
        TMP_InputField breakSecond = breakInputField.transform.Find("SecondInput").GetComponent<TMP_InputField>();
        string secondText = breakSecond.text;
        int seconds = 0;
        if (!string.IsNullOrEmpty(secondText) && int.TryParse(secondText, out seconds) == false)
        {
            Debug.LogError("Invalid second value: " + secondText);
            return;
        }
        seconds -= 5;
        breakSecond.text = seconds.ToString();
    }
    public void FocusTimeSetButtonClicked()
    {
        //인풋 필드의 값을 가져와 setFocusTime을 바꾼다.
        TMP_InputField focusMinute = focusInputField.transform.Find("MinuteInput").GetComponent<TMP_InputField>();
        TMP_InputField focusSecond = focusInputField.transform.Find("SecondInput").GetComponent<TMP_InputField>();
        string minuteText = focusMinute.text;
        string secondText = focusSecond.text;
        int minutes = 0;
        int seconds = 0;
        // 문자열이 비어있지 않고 숫자인 경우에만 파싱
        if (!string.IsNullOrEmpty(minuteText) && int.TryParse(minuteText, out minutes) == false)
        {
            Debug.LogError("Invalid minute value: " + minuteText);
            return;
        }

        if (!string.IsNullOrEmpty(secondText) && int.TryParse(secondText, out seconds) == false)
        {
            Debug.LogError("Invalid second value: " + secondText);
            return;
        }
        //위의 텍스트를 가져와 초로 변환 후 넣음
        setFocusTime = minutes * 60 + seconds;
        ChangeSetTimeText();
    }
    public void BreakTimeSetButtonClicked()
    {
        //인풋 필드의 값을 가져와 setFocusTime을 바꾼다.
        TMP_InputField breakMinute = breakInputField.transform.Find("MinuteInput").GetComponent<TMP_InputField>();
        TMP_InputField breakSecond = breakInputField.transform.Find("SecondInput").GetComponent<TMP_InputField>();
        string minuteText = breakMinute.text;
        string secondText = breakSecond.text;
        int minutes = 0;
        int seconds = 0;
        // 문자열이 비어있지 않고 숫자인 경우에만 파싱
        if (!string.IsNullOrEmpty(minuteText) && int.TryParse(minuteText, out minutes) == false)
        {
            Debug.LogError("Invalid minute value: " + minuteText);
            return;
        }

        if (!string.IsNullOrEmpty(secondText) && int.TryParse(secondText, out seconds) == false)
        {
            Debug.LogError("Invalid second value: " + secondText);
            return;
        }
        //위의 텍스트를 가져와 초로 변환 후 넣음
        setBreakTime = minutes * 60 + seconds;
        ChangeSetTimeText();
    }
    public void loopButtonClicked()
    {
        isLoop = !isLoop;
        if (isLoop)
        {
            //체크표시로 바꿈
        }
        else
        {
            //빈 공간으로 바꿈
        }
    }
    public void PlayResetButtonClicked()
    {
        isPlay = !isPlay;
        isFocus = !isFocus;
        currentTime = 0f;
        stopButtonPanel.SetActive(!stopButtonPanel.activeSelf);
        playButton.SetActive(!playButton.activeSelf);
        focusText.gameObject.SetActive(!focusText.gameObject.activeSelf);
        SetCurrentTimeText();
    }
    public void StopButtonClicked()
    {
        isPlay = !isPlay;
        TextMeshProUGUI text = stopButtonPanel.transform.Find("StopButton").Find("Text").GetComponent<TextMeshProUGUI>();
        if (isPlay)
        {
            text.text = stopTexts[settingManager.GetCurrentLanguage()];
        }
        else
        {
            text.text = restartTexts[settingManager.GetCurrentLanguage()];
        }
    }
    public void SettingButtonClicked()
    {
        pomodoroSettingPanel.SetActive(!pomodoroSettingPanel.activeSelf);
    }
}
