using UnityEngine;
using UnityEngine.EventSystems;

public class BtnScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Vector3 normalScale = Vector3.one;        // 기본 크기
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1f); // 마우스 올렸을 때 크기
    public float duration = 0.1f;                    // 크기 전환 속도

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = normalScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleTo(hoverScale));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(ScaleTo(normalScale));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"{gameObject.name} 버튼 클릭됨!");
    }

    private System.Collections.IEnumerator ScaleTo(Vector3 target)
    {
        Vector3 start = rectTransform.localScale;
        float time = 0;

        while (time < duration)
        {
            rectTransform.localScale = Vector3.Lerp(start, target, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        rectTransform.localScale = target;
    }
}
