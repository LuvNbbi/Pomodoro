using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorInfoPanelScript : MonoBehaviour
{
    DecorItem selectedDecor;
    int placedIndex;
    string parentName;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SetSelectedDecor(DecorItem decorItem, int chgPlacedIndex, string getParentName)
    {
        selectedDecor = decorItem;
        placedIndex = chgPlacedIndex;
        parentName = getParentName;
    }
    public void ToBagButtonClicked()
    {
        GameManager.GetInstance().RemovePlacedDecor(placedIndex, parentName);
        transform.parent.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
