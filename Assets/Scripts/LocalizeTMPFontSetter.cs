using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Localization.Components;

public class LocalizedTMPFontSetter : MonoBehaviour
{
    public LocalizedAsset<TMP_FontAsset> localizedFont; // FontTable과 Key 설정

    public TMP_Text targetText; // 폰트를 적용할 TextMeshPro UI

    private void OnEnable()
    {
        if (targetText == null) targetText = GetComponent<TMP_Text>();

        localizedFont.AssetChanged += UpdateFont;
        localizedFont.LoadAssetAsync().Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                targetText.font = handle.Result;
            }
        };
    }

    private void OnDisable()
    {
        localizedFont.AssetChanged -= UpdateFont;
    }

    private void UpdateFont(TMP_FontAsset newFont)
    {
        if (targetText != null && newFont != null)
        {
            targetText.font = newFont;
        }
    }
}