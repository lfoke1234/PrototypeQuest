using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameState_Boss : EnemyState
{
    Enemy_Boss enemy;

    public FlameState_Boss(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy = enemyBase as Enemy_Boss;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = enemy.flameDuration;
        enemy.agent.isStopped = true;
        enemy.agent.velocity = Vector3.zero;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.UseFlame();
    }

    public override void Update()
    {
        base.Update();

        enemy.transform.rotation = enemy.FaceTarget(enemy.player.position);

        if (stateTimer <= 0 && enemy.flameActive)
        {
            enemy.ActiveFlame(false);
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void SkillTrigger()
    {
        base.SkillTrigger();
        enemy.ActiveFlame(true);
    }
}
