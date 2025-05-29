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