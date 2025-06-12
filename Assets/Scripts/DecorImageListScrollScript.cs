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
    void Start()
    {
        if (content == null)
        {
            content = transform.Find("Viewport/Content").gameObject;
        }
        decorDict = GameManager.GetInstance().GetDecorDict();
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (string key in decorDict.Keys)
        {
            Decor decorItem = decorDict[key];
            GameObject decorImageGoods = Addressables.InstantiateAsync("DecorImageGoods").WaitForCompletion();
            decorImageGoods.transform.SetParent(content.transform, false);
            //이름 변경
            TextMeshProUGUI textObj = decorImageGoods.transform.Find("DecorNameText").GetComponent<TextMeshProUGUI>();
            textObj.text = decorItem.name[SettingManager.GetInstance().currentLanguage];

            //가격 변경
            textObj = decorImageGoods.transform.Find("DecorImagePriceText").GetComponent<TextMeshProUGUI>();
            textObj.text = decorItem.price.ToString();

            //이미지 변경
            Image image = decorImageGoods.transform.Find("Image").GetComponent<Image>();
            image.sprite = Addressables.LoadAssetAsync<Sprite>(decorItem.spriteName).WaitForCompletion();

        }
    }

    void Update()
    {

    }
}
