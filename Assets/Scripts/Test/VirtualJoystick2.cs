using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler,IDragHandler
{
    public Vector2 Value { get; private set; }
    private int pointerId;
    private bool isDragging = false;
    
    public void OnPointerUp(PointerEventData eventData)
    {
        if (pointerId != eventData.pointerId)
            return;

        isDragging = false;

        //stick.rectTransform.position = originalPoint;
        Value = Vector2.zero;
    }


    public void OnDrag(PointerEventData eventData)
    {
        Value = eventData.delta / Screen.dpi;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDragging)
            return;
        isDragging = true;
        pointerId = eventData.pointerId;

    }

}
