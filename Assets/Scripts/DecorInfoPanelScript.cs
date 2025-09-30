using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorInfoPanelScript : MonoBehaviour
{
    DecorItem selectedDecor;
    int placedIndex;
    string objName;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetSelectedDecor(DecorItem decorItem, string objName)
    {
        selectedDecor = decorItem;
        this.objName = objName;
    }
    public void ToBagButtonClicked()
    {
        GameManager.GetInstance().RemovePlacedDecor(selectedDecor, objName);
        transform.parent.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
