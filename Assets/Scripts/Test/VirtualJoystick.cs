using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Timeline;
using UnityEngine.UI;


public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    // Start is called before the first frame update
    public enum Axis
    {
        Horizontal,
        Vertical,
    }
    public GameObject OutlineImg;
    public Image stick;
    public float radius;
    private Vector3 originalPoint;
    private RectTransform rectTr;
    
    private Vector2 value;

    private int pointerId;
    private bool isDragging = false;


    public void Start()
    {
        rectTr = GetComponent<RectTransform>();
        originalPoint = stick.rectTransform.position;
        
    }

    public float GetAxis(Axis axis)
    {
        switch(axis)
        {
            case Axis.Horizontal:
                return value.x;
            case Axis.Vertical:
                return value.y;
        }
        return 0.0f;
    }

    private void Update()
    {
        //Debug.Log($"{GetAxis(Axis.Horizontal)}/{GetAxis(Axis.Vertical)}");
    }


    private void UpdateStickPos(Vector2 screenPos)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTr, screenPos, null, out Vector3 newPoint);
        var delta = Vector3.ClampMagnitude(newPoint - originalPoint, radius);
        value = delta / radius;

        stick.rectTransform.position = originalPoint + delta;
    }
        
    public void OnDrag(PointerEventData eventData)
    {
        if (pointerId != eventData.pointerId)
            return;

        Debug.Log(OutlineImg.transform.position);
        Debug.Log(stick.transform.position);
        UpdateStickPos(eventData.position);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isDragging)
            return;
        OutlineImg.SetActive(true);
        isDragging = true;
        pointerId = eventData.pointerId;
        UpdateStickPos(eventData.position);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (pointerId != eventData.pointerId)
            return;

        OutlineImg.SetActive(false);
        isDragging = false;
        
        stick.rectTransform.position = originalPoint;
        value = Vector2.zero;
        //throw new System.NotImplementedException();
    }
}
