using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILanguageManager : MonoBehaviour
{
    public Dictionary<string, List<string>> UILanguageDictionary = new Dictionary<string, List<string>>()
    {
        {"SettingPanelText", new List<string>() {"Setting", "설정", "設定"}},
        {"LanguagePanelText", new List<string>() {"Language", "언어", "言語"}},
        {"SoundPanelText", new List<string>() {"Sound", "소리", "音"}},
        {"ExitButtonText", new List<string>() {"Exit", "종료", "終了"}},
        {"CloseButtonText", new List<string>() {"Close", "닫기", "閉める"}},
        {"FocusText", new List<string>() {"Focus", "집중", "集中"}},
        {"SetTimeText", new List<string>() {"Focus Time - \nBreak Time - ", "집중 시간 - \n휴식 시간 - ", "集中時間 - \n休息時間 - "}},
        {"PlayButtonText", new List<string>() {"Start", "시작", "始める"}},
        {"StopButtonText", new List<string>() {"Stop", "일시정지", "止める"}},
        {"ResetButtonText", new List<string>() {"Reset", "초기화", "リセット"}},
        {"CurrentLanguageText", new List<string>() {"English", "한국어", "日本語" } },
        {"FocusTimeSetText", new List<string>() {"Change Focus Time", "집중 시간 변경", "集中時間変更" } },
        {"BreakTimeSetText", new List<string>() {"Change Break Time", "휴식 시간 변경", "休息時間変更" } },
        {"FocusTimeApplyText", new List<string>() {"Set", "설정", "設定" } },
        {"BreakTimeApplyText", new List<string>() {"Set", "설정", "設定" } },
        {"LoopButtonText", new List<string>() {"Loop", "반복하기", "リピート" } },
        {"ToDoListPanelName", new List<string>() {"To Do", "나의 목표", "私の目標" }},
        {"ToDoListAddPanelName", new List<string>() {"Write ToDoList", "목표 추가하기", "目標追加" }},
        {"ToDoNameText", new List<string>() {"ToDo", "목표", "目標" }},
        {"ToDoNameInputFieldPlace", new List<string>() {"Input Your ToDoList", "목표를 입력해주세요", "目標を入力してください" }},
        {"ToDoNameInputFieldText", new List<string>() {"", "", "" }},
        {"StartDayText", new List<string>() {"Start Date", "시작 날짜", "開始日" }},
        {"EndDayText", new List<string>() {"End Date", "목표 날짜", "終了日" }},
        {"RangeText", new List<string>() {"Range", "범위", "範囲" }},
        {"BackButtonText", new List<string>() {"Back", "취소", "キャンセル" }},
        {"AddButtonText", new List<string>() {"Add", "추가하기", "追加する" }},
    };
    // \        {"FocusTimeSetText", focusTimeSetText},
    //     {"BreakTimeSetText", breakTimeSetText},
    //     {"FocusTimeApplyText", focusTimeApplyText},
    //     {"BreakTimeApplyText", breakTimeApplyText},
    public List<string> GetUILanguage(string UIName)
    {
        return UILanguageDictionary[UIName];
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
