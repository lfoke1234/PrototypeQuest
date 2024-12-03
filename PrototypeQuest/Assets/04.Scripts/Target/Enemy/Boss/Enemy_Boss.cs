using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Boss : Enemy
{
    public Enemy_BossVisuals bossVisual { get; private set; }

    [Header("Attack info")]
    [SerializeField] private float attackRange;
    public float actionCooldown;

    [Header("Jump Attack Info")]
    public float jumpAttackCooldown;
    private float lastTimetoJump;
    public float timeToTarget = 1;
    public float minJumpDistance;
    [Space]
    public float impactRadius = 2.5f;
    [SerializeField] private int jumpDamage;
    public Transform impactPoint;

    [Header("Flame Info")]
    public float flameDuration;
    public ParticleSystem flame;
    public float flameCooldown;
    private float lastTimeToFlame;

    public bool flameActive {  get; private set; }
    public bool dontMove;

    #region State

    public IdleState_Boss idleState { get; private set; }
    public MoveState_Boss moveState { get; private set; }
    public AttackState_Boss attackState { get; private set; }
    public JumpAttackState_Boss jumpAttackState { get; private set; }
    public FlameState_Boss flameState { get; private set; }

    public Dead_Boss deadState { get; private set; }

    #endregion


    protected override void Awake()
    {
        base.Awake();
        bossVisual = GetComponent<Enemy_BossVisuals>();

        idleState = new IdleState_Boss(this, stateMachine, "Idle");
        moveState = new MoveState_Boss(this, stateMachine, "Move");

        attackState = new AttackState_Boss(this, stateMachine, "Attack");
        jumpAttackState = new JumpAttackState_Boss(this, stateMachine, "JumpAttack");
        flameState = new FlameState_Boss(this, stateMachine, "Flame");

        deadState = new Dead_Boss(this, stateMachine, "Dead");
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

    public bool PlayerInAttackRange() => Vector3.Distance(transform.position, player.position) < attackRange;

    #region JumpAttack
    public void JumpImpact()
    {
        Transform impactPoint = this.impactPoint;

        if (impactPoint == null)
            impactPoint = transform;

        Collider[] colliders = Physics.OverlapSphere(impactPoint.position, impactRadius);

        foreach (Collider hit in colliders)
        {
            Player player = hit.GetComponent<Player>();

            if (player != null)
                player.stat.TakeDamageWithValue(10);
        }
    }

    public bool CanJumpAttack()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < minJumpDistance)
            return false;

        if (Time.time > lastTimetoJump + jumpAttackCooldown)
        {
            return true;
        }

        return false;
    }

    public void UseJumpAttack() => lastTimetoJump = Time.time;
    #endregion

    public void ActiveFlame(bool active)
    {
        flameActive = active;

        if (!active)
        {
            flame.Stop();
            animator.SetTrigger("EndFlame");
            return;
        }


        var mainModule = flame.main;
        var extraModuel = flame.transform.GetChild(0).GetComponent<ParticleSystem>().main;

        mainModule.duration = flameDuration;
        extraModuel.duration = flameDuration;

        flame.Clear();
        flame.Play();
    }

    public bool CanUseFlame()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > minJumpDistance)
            return false;

        if (Time.time > lastTimeToFlame + flameCooldown)
        {
            return true;
        }

        return false;
    }

    public void UseFlame() => lastTimeToFlame = Time.time;

    public override void Die()
    {
        base.Die();
        stateMachine.ChangeState(deadState);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, minJumpDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, impactRadius);
    }
}
