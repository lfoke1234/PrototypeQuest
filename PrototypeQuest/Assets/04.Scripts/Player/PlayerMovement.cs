using System.Collections;
using System.Collections.Generic;
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

    private Vector3 initLocalPosition;

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
        if (player.playerAttack.isAttacking || DialogueManager.instance.isDialgoueActive)
            return;

        SetAnimation();

        MovetoKey();
        ApplyRotation();

        ApplyGravity();
    }

    private void LateUpdate()
    {
        player.animator.transform.localPosition = initLocalPosition;
    }

    #region Click Move

    private void MovetoRay()
    {
        if (player.playerAttack.isAttacking || DialogueManager.instance.isDialgoueActive)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            agent.enabled = true;
            agent.destination = hit.point;
        }
    }

    private bool MovetoTarget()
    {
        if (player.playerAttack.isAttacking || DialogueManager.instance.isDialgoueActive)
            return false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);

        foreach (RaycastHit hit in hits)
        {
            Target targetComponent = hit.collider.GetComponent<Target>();

            if (targetComponent != null)
            {
                Debug.Log("Hit Target");
                agent.enabled = true;
                agent.destination = hit.point;
                return true;
            }
        }
        return false;
    }
    #endregion

    #region Key Move
    private void MovetoKey()
    {
        if (moveInput != Vector2.zero)
        {
            agent.enabled = false;

            movementDirection = new Vector3(moveInput.x, 0, moveInput.y);

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
        Vector3 lookDirection = new Vector3(moveInput.x, 0, moveInput.y);
        lookDirection.y = 0;
        lookDirection.Normalize();

        if (lookDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }
    #endregion

    private void ApplyGravity()
    {
        if (!characterController.isGrounded)
        {
            movementDirection.y -= 9.81f * Time.deltaTime;
            characterController.Move(movementDirection * Time.deltaTime);
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

    private void AssignKey()
    {
        playerInput = player.playerInput;

        playerInput.Player.ClickMove.performed += ctx => { if (MovetoTarget()) return; MovetoRay(); };

        playerInput.Player.KeyMove.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInput.Player.KeyMove.canceled += ctx => moveInput = Vector2.zero;
    }
}
