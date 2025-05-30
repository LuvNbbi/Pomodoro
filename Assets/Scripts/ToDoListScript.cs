using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class ToDoListScript : MonoBehaviour
{
    public ToDoList toDoListInfo;
    public Button completeButton;
    public GameObject addLibraryObject;
    public Button addLibraryButton;
    public Button deleteButton;
    public Image Check;
    
    void Start()
    {
        completeButton = transform.Find("CompleteButton").GetComponent<Button>();
        completeButton.onClick.AddListener(CompleteButtonClicked);
        Check = transform.Find("CompleteButton").Find("Check").GetComponent<Image>();
        addLibraryObject = transform.Find("AddLibrary").gameObject;
        addLibraryButton = addLibraryObject.GetComponent<Button>();
        addLibraryButton.onClick.AddListener(AddLibraryButtonClicked);
        deleteButton = transform.Find("DeleteButton").GetComponent<Button>();
        deleteButton.onClick.AddListener(DeleteButtonClicked);
    }

    void CompleteButtonClicked()
    {
        toDoListInfo.isComplete = !toDoListInfo.isComplete;
        if (toDoListInfo.isComplete == false)
        {
            Check.sprite = null;
        }
        else
        {
            Check.sprite = Addressables.LoadAssetAsync<Sprite>("CheckImage").WaitForCompletion();
        }
        addLibraryObject.SetActive(toDoListInfo.isComplete);
    }

    void AddLibraryButtonClicked()
    {
        Debug.Log("책장에 추가");
    }

    void DeleteButtonClicked()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
