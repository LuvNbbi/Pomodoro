using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using TMPro;

public class HairSlotScript : MonoBehaviour, IPointerClickHandler
{
    public StyleItem styleItem;
    public BuyPanelScript buyPanelScript;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(styleItem.spriteName);
        GameManager.GetInstance().chgHair(styleItem.spriteName);
    }

    public void SetHairSlot(StyleItem setStyleItem)
    {
        styleItem = setStyleItem;
        Debug.Log($"{styleItem.spriteName} 이랑 {setStyleItem.spriteName}");
        //이미지 변경
        Image hairImage = transform.Find("Image").GetComponent<Image>();
        hairImage.sprite = Addressables.LoadAssetAsync<Sprite>(styleItem.spriteName).WaitForCompletion();

        //가격 변경
        TextMeshProUGUI priceText = transform.Find("PriceText").GetComponent<TextMeshProUGUI>();
        priceText.text = styleItem.price.ToString();

        //구매하지 않았다면 BuyPanel을 true로
        transform.Find("BuyPanel").gameObject.SetActive(!styleItem.isOwned);

        buyPanelScript.SetBuyPanel(styleItem);
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
