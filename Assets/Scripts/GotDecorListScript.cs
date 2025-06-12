using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
public class GotDecorListScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public DecorItem decorItem;
    public GameObject overviewPanel;
    public int index;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (overviewPanel == null)
        {
            overviewPanel = transform.Find("OverviewPanel").gameObject;
        }
        overviewPanel.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        overviewPanel.SetActive(false);
    }

    public void SetDecorItem(DecorItem chgDecorItem)
    {
        decorItem = chgDecorItem;
    }
    public void SetObject(DecorItem chgDecorItem, int i)
    {
        decorItem = chgDecorItem;
        index = i;
        //장식 이름 넣기
        TextMeshProUGUI tmp = transform.Find("DecorNameText").GetComponent<TextMeshProUGUI>();
        tmp.text = decorItem.name;
        //장식 이미지 넣기
        Image image = transform.Find("Image").GetComponent<Image>();
        image.sprite = Addressables.LoadAssetAsync<Sprite>(decorItem.spriteName).WaitForCompletion();
        //Overview 패널 채우기
        //Overview의 장식 이름
        tmp = transform.Find("OverviewPanel/Name").GetComponent<TextMeshProUGUI>();
        tmp.text = decorItem.name;
        //Overview의 날짜
        tmp = transform.Find("OverviewPanel/Date").GetComponent<TextMeshProUGUI>();
        tmp.text = decorItem.startDate + " ~ " + decorItem.endDate;
        //Overview의 메모
        tmp = transform.Find("OverviewPanel/Memo").GetComponent<TextMeshProUGUI>();
        tmp.text = decorItem.memo;
    }

    public void RemoveButtonClicked()
    {
        GameManager.GetInstance().RemoveGotDecorList(index);
    }
    public void PlaceButtonClicked()
    {
        GameManager.GetInstance().PlaceDecorItem(decorItem);
        GameManager.GetInstance().placeGotDecorList = GetComponent<GotDecorListScript>();
        GameManager.GetInstance().isPlaceToBag = true;
        UIManager.GetInstance().BagPanelControl();
    }
    void Start()
    {
        overviewPanel = transform.Find("OverviewPanel").gameObject;
        overviewPanel.SetActive(false);
    }


    void Update()
    {

    }
}
