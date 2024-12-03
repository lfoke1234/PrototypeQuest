using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInputHandler : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = PlayerManager.instance.player.playerMovement;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("Touch Began");

                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return;
                }

                HandleTouchMovement(touch.position);
            }
        }
    }

    private void HandleTouchMovement(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        Target target = null;
        foreach (RaycastHit hit in hits)
        {
            Target targetComponent = hit.collider.GetComponent<Target>();
            if (targetComponent != null)
            {
                target = targetComponent;
                break;
            }
        }

        if (target != null)
        {
            playerMovement.SetCurrentTarget(target);
        }
        else
        {
            playerMovement.MoveToPoint(ray);
        }
    }
}
