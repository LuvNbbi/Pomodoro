using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class DecorImageListScrollScript : MonoBehaviour
{
    public GameObject content;
    Dictionary<string, Decor> decorDict;

    public List<StyleItem> decorItems = new()
    {
        new StyleItem(){
            type = "Decor",
            name = "Decor_000",
            spriteName = "Decor_000",
            price = 0,
            isOwned = false,
            index = 0
        },
        new StyleItem(){
            type = "Decor",
            name = "Decor_001",
            spriteName = "Decor_001",
            price = 100,
            isOwned = false,
            index = 1
        },
        new StyleItem(){
            type = "Decor",
            name = "Decor_002",
            spriteName = "Decor_002",
            price = 100,
            isOwned = false,
            index = 2
        },
        new StyleItem(){
            type = "Decor",
            name = "Decor_003",
            spriteName = "Decor_003",
            price = 100,
            isOwned = false,
            index = 3
        },
    };
    public void SetDecorList()
    {
        if (content == null)
        {
            content = transform.Find("Viewport/Content").gameObject;
        }
        List<int> gotDecors = GameManager.GetInstance().playerInfo.gotDecors;
        Debug.Log($"GotDecor : {gotDecors[0]}");
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (StyleItem decorName in decorItems)
        {
            Debug.Log($"{decorName.name}, {decorName.index}");
            if (gotDecors.Contains(decorName.index))
            {
                Debug.Log($"{decorName.name}");
                decorName.isOwned = true;
            }
            GameObject decorSlot = Addressables.InstantiateAsync("DecorSlot").WaitForCompletion();
            decorSlot.transform.SetParent(content.transform, false);
            DecorSlotScript script = decorSlot.GetComponent<DecorSlotScript>();
            script.SetDecorSlot(decorName);
        }
    }
}
