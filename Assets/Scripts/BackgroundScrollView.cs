using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BackgroundScrollView : MonoBehaviour
{
    public void ClothesListOpen()
    {
        GameManager.GetInstance().CharacterListClose();
        transform.parent.parent.gameObject.SetActive(true);
    }
    public List<StyleItem> clothesItems = new List<StyleItem>()
    {
        new StyleItem(){
            type = "Clothes",
            name = "Clothes_000",
            spriteName = "Clothes_000",
            price = 0,
            isOwned = false,
            index = 0
        },
        new StyleItem(){
            type = "Clothes",
            name = "Clothes_001",
            spriteName = "Clothes_001",
            price = 100,
            isOwned = false,
            index = 1
        }
    };
    public List<StyleItem> bgItems = new List<StyleItem>()
    {
        new StyleItem(){
            type = "Background",
            name = "Background_000",
            spriteName = "Background_000",
            price = 0,
            isOwned = false,
            index = 0
        },
        new StyleItem(){
            type = "Background",
            name = "Background_001",
            spriteName = "Background_001",
            price = 100,
            isOwned = false,
            index = 1
        }
    };
    public void SetClothesList()
    {
        List<int> gotClothes = GameManager.GetInstance().playerInfo.gotClothes;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (StyleItem clothesName in clothesItems)
        {
            if (gotClothes.Contains(clothesName.index))
            {
                clothesName.isOwned = true;
            }
            GameObject clothesSlot = Addressables.InstantiateAsync("ClothesSlot").WaitForCompletion();
            clothesSlot.transform.SetParent(transform, false);
            ClothesSlotScript script = clothesSlot.GetComponent<ClothesSlotScript>();
            script.SetClothesSlot(clothesName);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
