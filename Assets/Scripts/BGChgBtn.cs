using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BGChgBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string bgNum;
    private string bgName;
    private string tnName;
    public Image TN;
    public GameObject lockImage;
    public GameObject buyBtn;
    public GameObject priceObj;
    public TextMeshProUGUI priceText;
    public int price;
    public bool isTN;
    void Start()
    {
        bgName = "Background_" + bgNum;
        tnName = "BG_TN_" + bgNum;
        TN.sprite = Addressables.LoadAssetAsync<Sprite>(tnName).WaitForCompletion();
        priceText.text = price.ToString();
        string gotbg = "start";
        foreach (string num in GameManager.GetInstance().playerInfo.gotBG)
        {
            gotbg += $" {num}, ";
        }
        Debug.Log(gotbg);
        Refresh();
    }
    public void chgBG()
    {
        if (bgNum == null) bgNum = "000";
        Refresh();
        if(GameManager.GetInstance().playerInfo.gotBG.Contains(bgNum))
            GameManager.GetInstance().ChgBackground(bgName);
    }
    public void BuyBtn()
    {
        int crtMoney = GameManager.GetInstance().playerInfo.money;
        if (crtMoney >= price)
        {
            GameManager.GetInstance().DecreaseGameMoney(price);
            GameManager.GetInstance().AddGotBG(bgNum);
            Refresh();
        }
    }
    public void Refresh()
    {
        string gotbg = "";
        foreach (string num in GameManager.GetInstance().playerInfo.gotBG)
        {
            gotbg += $" {num}, ";
        }
        Debug.Log($"{gameObject.name} refresh {gotbg}");
        if (GameManager.GetInstance().playerInfo.gotBG.Contains(bgNum))
        {

            //잠금 모양 해제
            lockImage.SetActive(false);
            buyBtn.SetActive(false);
            priceObj.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TN.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TN.gameObject.SetActive(false);
    }
}
