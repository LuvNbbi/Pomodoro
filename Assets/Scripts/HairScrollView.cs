using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
public class HairScrollView : MonoBehaviour
{
    public void HairListOpen()
    {
        GameManager.GetInstance().CharacterListClose();
        transform.parent.parent.gameObject.SetActive(true);
    }
    public List<StyleItem> hairItems = new List<StyleItem>()
    {
        new StyleItem(){
            type = "Hair",
            name = "Hair_000",
            spriteName = "Hair_000",
            price = 0,
            isOwned = false,
            index = 0
        },
        new StyleItem(){
            type = "Hair",
            name = "Hair_001",
            spriteName = "Hair_001",
            price = 100,
            isOwned = false,
            index = 1
        }
    };
    public void SetHairList()
    {
        List<int> gotHairs = GameManager.GetInstance().playerInfo.gotHairs;
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        foreach (StyleItem hairName in hairItems)
            {
                if (gotHairs.Contains(hairName.index))
                {
                    hairName.isOwned = true;
                }
                GameObject hairSlot = Addressables.InstantiateAsync("HairSlot").WaitForCompletion();
                hairSlot.transform.SetParent(transform, false);
                HairSlotScript script = hairSlot.GetComponent<HairSlotScript>();
                script.SetHairSlot(hairName);
                Debug.Log("생성 완료");
            }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
