using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private Vector3 initLocalPosition;

    [Header("Idle Info")]
    public float idleTime;
    public float aggrestionRange;

    [Header("Move Info")]
    public float moveSpeed;
    public float chaseSpeed;
    public float trunSpeed;
    private bool manuallyMovement;
    private bool manuallyRotation;

    [SerializeField] private Transform[] patrolPoints;
    private int currentPatrolIndex;

    public NavMeshAgent agent {  get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }
    public Animator animator { get; private set; }
    public Transform player { get; private set; }
    
    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    protected virtual void Start()
    {
        player = PlayerManager.instance.player.transform;
        InitializePatrolPoints();
        initLocalPosition = animator.transform.localPosition;
    }


    protected virtual void Update()
    {
    }

    private void LateUpdate()
    {
        animator.transform.localPosition = initLocalPosition;
    }

    public void AnimationTrigger()
    {
        stateMachine.currentState.AnimationTrigger();
    }

    public bool PlayerInAggrestionRange() => Vector3.Distance(transform.position, player.position) < aggrestionRange;
    

    public void ActiveManualMovemet(bool manualMovement) => this.manuallyMovement = manualMovement;
    public bool ManualMovementActive() => manuallyMovement;

    public void ActiveManualRotation(bool manualRotation) => this.manuallyRotation = manualRotation;
    public bool ManualRotationActive() => manuallyRotation;

    #region Patrol
    private void InitializePatrolPoints()
    {
        foreach (var p in patrolPoints)
        {
            p.parent = null;
        }
    }

    public Vector3 GetPatrolDestination()
    {
        Vector3 destination = patrolPoints[currentPatrolIndex].transform.position;

        currentPatrolIndex++;

        if (currentPatrolIndex >= patrolPoints.Length)
            currentPatrolIndex = 0;

        return destination;
    }
    #endregion

    public Quaternion FaceTarget(Vector3 target)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);

        Vector3 currentEulerAngels = transform.rotation.eulerAngles;

        float yRotation = Mathf.LerpAngle(currentEulerAngels.y, targetRotation.eulerAngles.y, trunSpeed * Time.deltaTime);

        return Quaternion.Euler(currentEulerAngels.x, yRotation, currentEulerAngels.z);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, aggrestionRange);

        
    }
}
