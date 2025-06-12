using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressablesAssets;

public class GotDecorListScrollScript : MonoBehaviour
{
    public GameObject content;
    void Start()
    {
        content = transform.Find("Viewport/Content").gameObject;
        GameManager.GetInstance().gotDecorListScrollScript = GetComponent<GotDecorListScrollScript>();
    }

    public void AddGotDecorList(DecorItem decorItem)
    {
        if (content == null)
        {
            content = transform.Find("Viewport/Content").gameObject;
        }
        GameObject newGotDecorListObj = Addressables.InstantiateAsync("GotDecorList").WaitForCompletion();
        newGotDecorListObj.transform.SetParent(content.transform, false);
        GotDecorListScript script = newGotDecorListObj.GetComponent<GotDecorListScript>();
        script.SetObject(decorItem);
    }
}
