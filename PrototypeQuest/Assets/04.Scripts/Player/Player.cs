using UnityEngine;
using UnityEngine.TextCore.Text;

public class Player : MonoBehaviour
{
    public Animator animator { get; private set; }
    public PlayerInputController playerInput { get; private set; }
    public PlayerMovement playerMovement { get; private set; }
    public PlayerAttack playerAttack { get; private set; }
    public PlayerCollisionCheck check { get; private set; }

    public CharacterStat stat { get; private set; }

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerInput = new PlayerInputController();
        playerMovement = GetComponent<PlayerMovement>();
        playerAttack = GetComponent<PlayerAttack>();
        check = GetComponent<PlayerCollisionCheck>();

        stat = GetComponent<CharacterStat>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    public void Die()
    {
        animator.SetTrigger("Die");
    }
}
