using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private Player player;
    private PlayerInputController playerInput;
    private CharacterController characterController;
    private NavMeshAgent agent;

    [Header("Click Move Info")]
    [SerializeField] private Transform target;
    private Ray lastRay;

    [Header("Key Move Info")]
    private Vector3 movementDirection;
    public Vector2 moveInput { get; private set; }
    private bool keyMove;

    [SerializeField] private JoystickHandle joystickHandle;

    private Vector3 initLocalPosition;
    private Target currentTarget;
    [SerializeField] private bool allStop;
    private float verticalVelocity;

    private void Start()
    {
        player = GetComponent<Player>();
        characterController = GetComponent<CharacterController>();
        agent = GetComponent<NavMeshAgent>();

        initLocalPosition = player.animator.transform.localPosition;

        AssignKey();
    }

    private void Update()
    {
        if (player.playerAttack.isAttacking || DialogueManager.instance.isDialgoueActive || GameManager.Instance.isPlayCutScene || player.stat.isDead)
        {
            agent.enabled = false;
            return;
        }

        if (joystickHandle != null && keyMove == false)
        {
            moveInput = joystickHandle.InputVector;
        }

        SetAnimation();

        if (agent.enabled && currentTarget != null)
        {
            if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
            {
                currentTarget.InteractionEvent();
                currentTarget = null;
            }
        }

        MovetoKey();
        ApplyRotation();
    }


    private void LateUpdate()
    {
        player.animator.transform.localPosition = initLocalPosition;
    }

    #region Click Move

    private void MovetoRay()
    {
        if (player.playerAttack.isAttacking || DialogueManager.instance.isDialgoueActive || allStop || GameManager.Instance.isPlayCutScene || player.stat.isDead)
            return;

        SyncAgentAndDisableController();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            agent.enabled = true;
            agent.destination = hit.point;
        }
    }

    private Target MovetoTarget()
    {
        if (player.playerAttack.isAttacking || DialogueManager.instance.isDialgoueActive || allStop || GameManager.Instance.isPlayCutScene || player.stat.isDead)
            return null;

        SyncAgentAndDisableController();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            Target targetComponent = hit.collider.GetComponent<Target>();

            if (targetComponent != null)
            {
                agent.enabled = true;
                agent.destination = targetComponent.transform.position;
                agent.stoppingDistance = 1.5f;
                return targetComponent;
            }
        }
        return null;
    }

    private void SyncAgentAndDisableController()
    {
        if (characterController.enabled)
        {
            characterController.enabled = false;
            agent.enabled = true;
            agent.Warp(transform.position);
        }
    }

    #endregion

    #region Key Move
    private void MovetoKey()
    {
        if (moveInput != Vector2.zero)
        {
            agent.enabled = false;
            characterController.enabled = true;

            Transform camTransform = Camera.main.transform;
            Vector3 forward = camTransform.forward;
            Vector3 right = camTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            movementDirection = forward * moveInput.y + right * moveInput.x;

            ApplyGravity();

            if (movementDirection.magnitude > 0)
            {
                characterController.Move(movementDirection * Time.deltaTime * 5f);
            }
        }
        else
        {
            movementDirection = Vector3.zero;
        }
    }

    private void ApplyRotation()
    {
        Transform camTransform = Camera.main.transform;
        Vector3 forward = camTransform.forward;
        Vector3 right = camTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 lookDirection = forward * moveInput.y + right * moveInput.x;

        if (lookDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    #endregion

    #region Mobile Touch Move

    public void SetCurrentTarget(Target target)
    {
        currentTarget = target;
        agent.enabled = true;
        agent.destination = target.transform.position;
        agent.stoppingDistance = 1.5f;
    }

    public void MoveToPoint(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            SyncAgentAndDisableController();
            agent.enabled = true;
            agent.destination = hit.point;
        }
    }

    #endregion

    private void ApplyGravity()
    {
        if (characterController.isGrounded == false)
        {
            verticalVelocity -= 9.81f * Time.deltaTime;
            movementDirection.y = verticalVelocity;
        }
        else
        {
            verticalVelocity = -0.5f;
        }
    }

    private void SetAnimation()
    {
        Vector3 velocity = Vector3.zero;

        if (agent.enabled)
        {
            velocity = agent.velocity;
        }
        else
        {
            velocity = movementDirection * 5f;
        }

        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        player.animator.SetFloat("forwardSpeed", localVelocity.z);
    }


    public void Teleport(Vector3 position)
    {
        agent.enabled = false;
        characterController.enabled = false;

        transform.position = position;

        agent.Warp(position);
        agent.enabled = true;
    }

    private void AssignKey()
    {
        playerInput = player.playerInput;

        playerInput.Player.ClickMove.performed += ctx =>
        {
            Target target = MovetoTarget();
            if (target != null)
            {
                currentTarget = target;
            }   
            else
            {
                MovetoRay();
            }
        };

        playerInput.Player.KeyMove.performed += ctx =>
        { 
            moveInput = ctx.ReadValue<Vector2>();
            keyMove = true;
        };
        playerInput.Player.KeyMove.canceled += ctx =>
        {
            moveInput = Vector2.zero;
            keyMove = false;
        };

    }

}
