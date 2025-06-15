using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
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
    public string hair;
    public string clothes;
    public string backGround;
    public List<int> gotHairs;
    public List<int> gotClothes;
    public Dictionary<string, ToDoList> toDoLists;
    public Dictionary<string, PlacedFurnitureInfo> furnitures;
    public List<DecorItem> decorInventory;
    public Dictionary<string, bool> decor;
    public UserSetting userSetting;
}
[Serializable]
public class PlacedFurnitureInfo
{
    public string prefabName;
    public float x;
    public float y;
    public int siblingIndex;
    public Dictionary<string, DecorItem> placedItems;
}
[Serializable]
public class Decor
{
    public List<string> name;
    public string spriteName;
    public int price;
    public int level;
}

[Serializable]
public class DecorItem
{
    public string name;
    public string spriteName;
    public string startDate;
    public string endDate;
    public string memo;
}
[Serializable]
public class StyleItem
{
    public string type;
    public string name;
    public string spriteName;
    public int price;
    public bool isOwned;
    public int index;
}