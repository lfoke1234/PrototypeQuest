using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickHandle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;
    [SerializeField, Range(10f, 150f)]
    private float leverRange;

    private Vector2 inputVector;
    private bool isInput;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        isInput = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        isInput = true;
    }

    public void ControlJoystickLever(PointerEventData eventData)
    {
        Vector2 localPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            var inputDir = localPoint;
            var clampedDir = inputDir.magnitude < leverRange ? inputDir
                : inputDir.normalized * leverRange;
            lever.anchoredPosition = clampedDir;
            inputVector = clampedDir / leverRange;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        inputVector = Vector2.zero;
        isInput = false;
    }

    private void OnEnable()
    {
        lever.anchoredPosition = Vector2.zero;
        inputVector = Vector2.zero;
    }

    private void OnDisable()
    {
        lever.anchoredPosition = Vector2.zero;
        inputVector = Vector2.zero;
    }

    public void ResetJoystick()
    {
        lever.anchoredPosition = Vector2.zero;
        inputVector = Vector2.zero;
        isInput = false;
    }

    public Vector2 InputVector
    {
        get { return inputVector; }
    }
}
