using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Vector3 initLocalPosition;

    [Header("Idle Info")]
    public float idleTime;
    public float aggrestionRange;

    [Header("Move Info")]
    public float moveSpeed;
    public float chaseSpeed;
    public float turnSpeed;
    private bool manuallyMovement;
    private bool manuallyRotation;

    [SerializeField] private Transform[] patrolPoints;

    [Header("Attack Info")]
    public Transform attackCheck;
    public Vector3 attackSize;

    public bool dontPatrol;
    private int currentPatrolIndex;

    public NavMeshAgent agent { get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }
    public Animator animator { get; private set; }
    public Transform player { get; private set; }
    public EnemyStat stat { get; private set; }


    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        stat = GetComponent<EnemyStat>();
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


    public bool PlayerInAggrestionRange() => Vector3.Distance(transform.position, player.position) < aggrestionRange;

    #region Animation Trigger
    public void AnimationTrigger() => stateMachine.currentState.AnimationTrigger();

    public void ActiveManualMovemet(bool manualMovement) => this.manuallyMovement = manualMovement;
    public bool ManualMovementActive() => manuallyMovement;

    public void ActiveManualRotation(bool manualRotation) => this.manuallyRotation = manualRotation;
    public bool ManualRotationActive() => manuallyRotation;

    public virtual void SkillTrigger()
    {
        stateMachine.currentState.SkillTrigger();
    }
    #endregion

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

    #region Hit
    public virtual void GetHit()
    {

    }

    public virtual void HitImpact(Vector3 force, Rigidbody rb)
    {
        StartCoroutine(ImpactCourutine(force, rb));
    }

    private IEnumerator ImpactCourutine(Vector3 force, Rigidbody rb)
    {
        yield return new WaitForSeconds(0.1f);
        rb.AddForce(force, ForceMode.Impulse);
    }
    #endregion

    public Quaternion FaceTarget(Vector3 target, float turnSpeed = 0)
    {
        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);

        Vector3 currentEulerAngels = transform.rotation.eulerAngles;

        if(turnSpeed == 0)
        {
            turnSpeed = this.turnSpeed;
        }

        float yRotation = Mathf.LerpAngle(currentEulerAngels.y, targetRotation.eulerAngles.y, this.turnSpeed * Time.deltaTime);

        return Quaternion.Euler(currentEulerAngels.x, yRotation, currentEulerAngels.z);
    }

    public virtual void Die()
    {

    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, aggrestionRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackCheck.position, attackSize);
    }
}
