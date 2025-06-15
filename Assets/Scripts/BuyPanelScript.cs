using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyPanelScript : MonoBehaviour, IPointerClickHandler
{
    public StyleItem slotStyleItem;
    public void SetBuyPanel(StyleItem styleItem)
    {
        slotStyleItem = styleItem;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.GetInstance().playerInfo.money >= slotStyleItem.price)
        {
            //클릭 시 구매 UI가 나옴
            if (slotStyleItem.type == "Hair")
            {
                GameManager.GetInstance().playerInfo.gotHairs.Add(slotStyleItem.index);
                GameManager.GetInstance().SavePlayerInfo();
                transform.parent.parent.GetComponent<HairScrollView>().SetHairList();
            }
            else if (slotStyleItem.type == "Clothes")
            {
                GameManager.GetInstance().playerInfo.gotClothes.Add(slotStyleItem.index);
                GameManager.GetInstance().SavePlayerInfo();
                transform.parent.parent.GetComponent<ClothesScrollView>().SetClothesList();
            }
            GameManager.GetInstance().playerInfo.money -= slotStyleItem.price;
            UIManager.GetInstance().RefreshMoney();
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
