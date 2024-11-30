using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState_Melee : EnemyState
{
    Enemy_Melee enemy;
    private float lastTimeUpdatedDistanation;

    public ChaseState_Melee(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy = enemyBase as Enemy_Melee;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.agent.speed = enemy.chaseSpeed;
        enemy.agent.isStopped = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.PlayerInAttackRange())
            stateMachine.ChangeState(enemy.attackState);

        enemy.transform.rotation = enemy.FaceTarget(GetNextPathPoint());

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
