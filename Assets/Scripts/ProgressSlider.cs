using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProgressSlider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public SongController songController;
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        songController.isDragging = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        songController.isDragging = false;
        songController.SeekTo(slider.value);
    }
}
