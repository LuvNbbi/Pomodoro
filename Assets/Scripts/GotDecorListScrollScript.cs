using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class GotDecorListScrollScript : MonoBehaviour
{
    public GameObject content;
    void Start()
    {
        content = transform.Find("Viewport/Content").gameObject;
        GameManager.GetInstance().gotDecorListScrollScript = GetComponent<GotDecorListScrollScript>();
    }

    public void RefreshDecorList()
    {
        //플레이어 정보의 decorInventory를 보고 넣기
        List<DecorItem> decorItems = GameManager.GetInstance().GetDecorInventory();

        if (content == null)
        {
            content = transform.Find("Viewport/Content").gameObject;
        }
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }
        int i = 0;
        foreach (DecorItem decorItem in decorItems)
        {
            GameObject newGotDecorListObj = Addressables.InstantiateAsync("GotDecorList").WaitForCompletion();
            newGotDecorListObj.transform.SetParent(content.transform, false);
            GotDecorListScript script = newGotDecorListObj.GetComponent<GotDecorListScript>();
            script.SetObject(decorItem, i);
            i++;
        }
    }
}
