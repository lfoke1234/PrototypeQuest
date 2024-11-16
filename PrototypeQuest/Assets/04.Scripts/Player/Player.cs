using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator { get; private set; }
    public PlayerInputController playerInput { get; private set; }
    public PlayerMovement playerMovement { get; private set; }
    public PlayerAttack playerAttack { get; private set; }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerInput = new PlayerInputController();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
    }



    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}
