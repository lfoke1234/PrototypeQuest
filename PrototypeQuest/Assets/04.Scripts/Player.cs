using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController controller { get; private set; }

    private void Awake()
    {
        controller = new CharacterController();
    }

    private void OnEnable()
    {
        controller.Enable();
    }

    private void OnDisable()
    {
        controller.Disable();
    }
}
