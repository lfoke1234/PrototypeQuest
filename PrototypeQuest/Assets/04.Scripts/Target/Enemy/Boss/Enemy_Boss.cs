using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Boss : Enemy
{
    [Header("Attack info")]
    [SerializeField] private float attackRange;

    [Header("Jump Attack Info")]
    public float jumpAttackCooldown;
    private float lastTimetoJump;
    public float timeToTarget = 1;
    public float minJumpDistance;

    #region State

    public IdleState_Boss idleState { get; private set; }
    public MoveState_Boss moveState { get; private set; }
    public AttackState_Boss attackState { get; private set; }
    public JumpAttackState_Boss jumpAttackState { get; private set; }

    #endregion


    protected override void Awake()
    {
        base.Awake();
        idleState = new IdleState_Boss(this, stateMachine, "Idle");
        moveState = new MoveState_Boss(this, stateMachine, "Move");

        attackState = new AttackState_Boss(this, stateMachine, "Attack");
        jumpAttackState = new JumpAttackState_Boss(this, stateMachine, "JumpAttack");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(moveState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();

        if (Input.GetKeyDown(KeyCode.T))
            stateMachine.ChangeState(jumpAttackState);
    }

    public bool PlayerInAttackRange() => Vector3.Distance(transform.position, player.position) < attackRange;

    public bool CanJumpAttack()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < minJumpDistance)
            return false;

        if (Time.time > lastTimetoJump + jumpAttackCooldown)
        {
            lastTimetoJump = Time.time;
            return true;
        }

        return false;
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, minJumpDistance);
    }
}
