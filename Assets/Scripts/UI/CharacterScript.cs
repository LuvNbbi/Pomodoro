using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterScript : MonoBehaviour, IPointerClickHandler
{
    public CanvasGroup dialogCanvasGroup;
    public TextMeshProUGUI dialogText;
    public UIFader dialogUIFader;
    public float waitSeconds = 2f;
    public List<string> dialogs;
    public bool isTalk;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isTalk) return;
        int randNum = Random.Range(0, dialogs.Count);
        dialogText.text = dialogs[randNum];
        StartCoroutine(OnDialog());
    }

    IEnumerator OnDialog()
    {
        dialogUIFader.FadeIn();
        isTalk = true;
        yield return new WaitForSeconds(waitSeconds);
        dialogUIFader.FadeOut();
        isTalk = false;
    }
}
