using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.AddressableAssets;
using TMPro;

public class PlacePointScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public string itemName;
    public DecorItem decorItem;
    public Image decorImage;
    public GameObject decorNamePanel;
    public TextMeshProUGUI decorNameText;
    public bool isPlaced;
    public string placedIndex;
    public void OnPointerClick(PointerEventData eventData)
    {
        //ResetFields()
        //Destroy
        if (GameManager.GetInstance().isPlaceMode && !isPlaced)
        {
            decorItem = GameManager.GetInstance().placeDecorItem;
            decorImage.sprite = Addressables.LoadAssetAsync<Sprite>(decorItem.spriteName).WaitForCompletion();
            decorNameText.text = decorItem.name;
            GameManager.GetInstance().playerInfo.furnitures[gameObject.transform.parent.name].placedItems.Add(placedIndex, decorItem);
            GameManager.GetInstance().SavePlayerInfo();
            GameManager.GetInstance().EndPlaceMode();
            isPlaced = true;
        }
        else
        {
            //상세 정보가 보이는 UI를 킴
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (GameManager.GetInstance().isPlaceMode && !isPlaced)
        {
            //배치 모드 일 때 작업 실행
            decorItem = GameManager.GetInstance().placeDecorItem;
            decorImage.sprite = Addressables.LoadAssetAsync<Sprite>(decorItem.spriteName).WaitForCompletion();
            Color color = decorImage.color;
            color.a = 1f;
            decorImage.color = color;
        }
        else
        {
            //목표 등이 보이게
            if (isPlaced)
            {
                decorNamePanel.SetActive(true);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (GameManager.GetInstance().isPlaceMode && !isPlaced)
        {
            //배치 모드 일 때 작업 실행
            decorImage.sprite = null;
            Color color = decorImage.color;
            color.a = 0f;
            decorImage.color = color;
        }
        else
        {
            //목표 등이 안보이게
            if (isPlaced)
            {
                decorNamePanel.SetActive(false);
            }
        }
    }

    public void SetPlaceItemInfo(DecorItem setDecorItem)
    {
        if (decorImage == null)
        {
            decorImage = transform.GetComponent<Image>();
            decorNamePanel = transform.Find("DecorNamePanel").gameObject;
            decorNameText = decorNamePanel.transform.Find("DecorNameText").GetComponent<TextMeshProUGUI>();
        }
        decorItem = setDecorItem;
        decorImage.sprite = Addressables.LoadAssetAsync<Sprite>(decorItem.spriteName).WaitForCompletion();
        Color color = decorImage.color;
        color.a = 1f;
        decorImage.color = color;
        decorNameText.text = decorItem.name;
        isPlaced = true;
    }
    public void DeletePlaceItem()
    {
        decorItem = new DecorItem();
        decorImage.sprite = null;
        Color color = decorImage.color;
        color.a = 0f;
        decorImage.color = color;
        decorNameText.text = "";
        isPlaced = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        decorImage = transform.GetComponent<Image>();
        decorNamePanel = transform.Find("DecorNamePanel").gameObject;
        decorNameText = decorNamePanel.transform.Find("DecorNameText").GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {

    }
}
