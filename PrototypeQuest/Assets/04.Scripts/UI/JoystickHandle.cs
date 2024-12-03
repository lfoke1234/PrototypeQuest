using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickHandle : MonoBehaviour
{
    private RectTransform background;
    private RectTransform handle;

    public Vector2 InputDirection { get; private set; }

    private void Start()
    {
        background = GetComponent<RectTransform>();
        handle = transform.GetChild(0).GetComponent<RectTransform>();
        InputDirection = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out position);

        position.x = (position.x / background.sizeDelta.x);
        position.y = (position.y / background.sizeDelta.y);

        float x = position.x * 2 - 1;
        float y = position.y * 2 - 1;

        InputDirection = new Vector2(x, y);
        InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

        // Handle 위치 설정
        handle.anchoredPosition = new Vector2(InputDirection.x * (background.sizeDelta.x / 3), InputDirection.y * (background.sizeDelta.y / 3));
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputDirection = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
    }
}
