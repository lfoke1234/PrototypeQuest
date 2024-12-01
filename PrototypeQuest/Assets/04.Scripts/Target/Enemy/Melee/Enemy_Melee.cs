using UnityEngine;

[System.Serializable]
public struct AttackData
{
    public float attackRange;
    public float moveSpeed;
    public float attackIndex;
    [Range(1, 2)]
    public float animationSpeed;
}

public class Enemy_Melee : Enemy
{
    [SerializeField] private bool gizmos;

    public IdleState_Melee idleState { get; private set; }
    public MoveState_Melee moveState { get; private set; }
    public RecoveryState_Melee recoveryState { get; private set; }
    public ChaseState_Melee chaseState { get; private set; }
    public AttackState_Melee attackState { get; private set; }
    public DeadState_Melee deadState { get; private set; }
    public ThrowState_Melee throwState { get; private set; }

    [Header("AttackData")]
    public AttackData attackData;
    public bool typeIsRush;
    public bool typeIsThrow;

    [Header("Throw Info")]
    public GameObject throwKnifePrefab;
    public Transform throwPoint;
    public float throwingCooldown;
    public float throwKnifeSpeed;
    public float throwKnifeTimer;
    [SerializeField] private GameObject showKinfe;

    protected override void Awake()
    {
        base.Awake();

        idleState = new IdleState_Melee(this, stateMachine, "Idle");
        moveState = new MoveState_Melee(this, stateMachine, "Move");
        recoveryState = new RecoveryState_Melee(this, stateMachine, "Recovery");
        chaseState = new ChaseState_Melee(this, stateMachine, "Chase");

        attackState = new AttackState_Melee(this, stateMachine, "Attack");
        throwState = new ThrowState_Melee(this, stateMachine, "Throw");

        deadState = new DeadState_Melee(this, stateMachine, "Idle");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public bool PlayerInAttackRange() => Vector3.Distance(transform.position, player.position) < attackData.attackRange;

    public override void SkillTrigger()
    {
        base.SkillTrigger();
        showKinfe.SetActive(false);
    }

    public void ShowWeapon()
    {
        showKinfe.SetActive(true);
    }    

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);

    }

    protected override void OnDrawGizmos()
    {
        if (gizmos == false)
            return;

        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackData.attackRange);
    }
}
