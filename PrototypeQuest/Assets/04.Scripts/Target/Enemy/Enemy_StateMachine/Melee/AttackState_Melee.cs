using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class AttackState_Melee : EnemyState
{
    Enemy_Melee enemy;
    private Vector3 attackDirection;
    private float attackMoveSpeed;

    private const float MAX_ATTACK_DISTANCE = 50f;

    public AttackState_Melee(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy = enemyBase as Enemy_Melee;
    }

    public override void Enter()
    {
        base.Enter();
        attackMoveSpeed = enemy.attackData.moveSpeed;
        enemy.animator.SetFloat("AttackAnimationSpeed", enemy.attackData.animationSpeed);
        enemy.animator.SetFloat("AttackIndex", enemy.attackData.attackIndex);

        enemy.agent.isStopped = true;
        enemy.agent.velocity = Vector3.zero;

        attackDirection = enemy.transform.position + (enemy.transform.forward * MAX_ATTACK_DISTANCE);
    }

    public override void Exit()
    {
        base.Exit();

        int recoveryIndex = enemy.typeIsRush ? 0 : 1;
        enemy.animator.SetFloat("RecoveryIndex", recoveryIndex);
    }

    public override void Update()
    {
        base.Update();
        if (enemy.ManualRotationActive())
        {
            enemy.transform.rotation = enemy.FaceTarget(enemy.player.position);
            //attackDirection = enemy.transform.position + (enemy.transform.forward * MAX_ATTACK_DISTANCE);
        }

        if (enemy.ManualMovementActive())
        {
            enemy.transform.position = 
                Vector3.MoveTowards(enemy.transform.position, attackDirection, attackMoveSpeed * Time.deltaTime);
        }
        

        if (triggerCalled)
        {
            if (enemy.typeIsRush)
                stateMachine.ChangeState(enemy.recoveryState);
            else
                stateMachine.ChangeState(enemy.chaseState);
        }
    }


}
