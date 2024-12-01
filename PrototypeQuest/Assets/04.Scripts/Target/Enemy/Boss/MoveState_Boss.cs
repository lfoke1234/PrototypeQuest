using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState_Boss : EnemyState
{
    Enemy_Boss enemy;
    bool fast;
    private float lastTimeUpdatedDistanation;

    public MoveState_Boss(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy = enemyBase as Enemy_Boss;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.agent.speed = enemy.moveSpeed;
        enemy.agent.isStopped = false;

        enemy.agent.SetDestination(enemy.player.position);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.transform.rotation = enemy.FaceTarget(GetNextPathPoint());

        if (enemy.CanJumpAttack())
        {
            stateMachine.ChangeState(enemy.jumpAttackState);
        }
        else if (enemy.PlayerInAttackRange())
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        
        if (CanUpdateDestionation())
        {
            enemy.agent.destination = enemy.player.transform.position;
        }
    }

    private bool CanUpdateDestionation()
    {
        if (Time.time > lastTimeUpdatedDistanation + 0.25f)
        {
            lastTimeUpdatedDistanation = Time.time;
            return true;
        }

        return false;
    }
}
