using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;

public class FollowMouse : MonoBehaviour
{
    private Camera mainCamera;
    public Image decorItemImage;
    Vector3 mousePos;
    bool isPlaceMode;
    void Start()
    {
        mainCamera = Camera.main;
        decorItemImage = transform.GetComponent<Image>();
    }
    public void ShowFollowImage(string imageName)
    {
        decorItemImage.sprite = Addressables.LoadAssetAsync<Sprite>(imageName).WaitForCompletion();
        Color color = decorItemImage.color;
        color.a = 1f;
        decorItemImage.color = color;
        isPlaceMode = true;
    }
    public void HideFollowImage()
    {
        Color color = decorItemImage.color;
        color.a = 0f;
        decorItemImage.color = color;
        isPlaceMode = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (isPlaceMode)
        {
            mousePos = Input.mousePosition;

            transform.position = mousePos;
        }
    }
}
