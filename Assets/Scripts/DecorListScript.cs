using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DecorListScript : MonoBehaviour, IPointerClickHandler
{
    public string decorName;
    CreateDecoPanelScript createDecoPanelScript;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("데코리스트");
        if (createDecoPanelScript == null)
        {
            createDecoPanelScript = GameObject.Find("Canvas").transform.Find("CreateDecoPanelBack/CreateDecoPanel").GetComponent<CreateDecoPanelScript>();
        }
        //CreateDecoPanel의 DecoImageBG의 DecoImage 변경
        createDecoPanelScript.SetImage(decorName);
        //데코리스트 닫기
        createDecoPanelScript.DecorSelectPanelControl();
    }

    void Start()
    {
        createDecoPanelScript = GameObject.Find("Canvas").transform.Find("CreateDecoPanelBack/CreateDecoPanel").GetComponent<CreateDecoPanelScript>();
    }

    
    void Update()
    {
        
    }
}
