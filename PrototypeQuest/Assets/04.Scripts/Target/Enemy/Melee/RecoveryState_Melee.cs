using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryState_Melee : EnemyState
{
    Enemy_Melee enemy;

    public RecoveryState_Melee(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy = enemyBase as Enemy_Melee;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.agent.isStopped = true;
        stateTimer = enemy.throwingCooldown;

        if (enemy.typeIsThrow && enemy.animator.GetFloat("RecoveryIndex") == 1f)
        {
            enemy.ShowWeapon();
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        enemy.transform.rotation = enemy.FaceTarget(enemy.player.position);


        if (triggerCalled && enemy.typeIsThrow == false)
        {
            if (enemy.PlayerInAttackRange())
            {
                enemy.stateMachine.ChangeState(enemy.attackState);
            }
            else
            {
                enemy.stateMachine.ChangeState(enemy.chaseState);

            }
        }
        else if (stateTimer <= 0 && enemy.typeIsThrow)
        {
            enemy.stateMachine.ChangeState(enemy.throwState);
        }
    }
}
