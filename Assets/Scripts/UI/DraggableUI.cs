using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas == null)
            Debug.LogError("DraggableUI: No parent Canvas found! Please put this inside a Canvas.");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canvas == null)
            return;

        // Move the image according to drag delta
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
