using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ToDoList
{
    public string toDoName;
    public string startDate;
    public string endDate;
    public int startRange;
    public int endRange;
    public int nowRange;
    public bool isComplete;
}

[Serializable]
public class UserSetting
{
    public int sound;
    public int language;
}
[Serializable]
public class PlayerInfo
{
    public int money;
    public int level;
    public int exp;
    public Dictionary<string, List<Item>> furnitures;
    public List<Item> inventory;
}

[Serializable]
public class Item
{
    public List<string> name;
    public string type;
    public Dictionary<string, List<string>> info; // {목표 이름, 시작 날짜, 완료 날짜, 코멘트}
}