using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class JumpAttackState_Boss : EnemyState
{
    private Enemy_Boss enemy;
    private Vector3 lastPlayerPosition;

    private float jumpAttackMovementSpeed;

    public JumpAttackState_Boss(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy = enemyBase as Enemy_Boss;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.agent.isStopped = true;
        lastPlayerPosition = enemy.player.position;

        enemy.bossVisual.PlaceLandindZone(lastPlayerPosition);

        float distanceToPlayer = Vector3.Distance(lastPlayerPosition, enemy.transform.position);
        jumpAttackMovementSpeed = distanceToPlayer / enemy.timeToTarget;

        enemy.transform.rotation = enemy.FaceTarget(lastPlayerPosition, 1000);
    }

    public override void Exit()
    {
        base.Exit();
        enemy.UseJumpAttack();
    }

    public override void Update()
    {
        base.Update();

        Vector3 myPos = enemy.transform.position;
        enemy.agent.enabled = !enemy.ManualMovementActive();

        if(enemy.ManualMovementActive())
        {
            enemy.transform.position = Vector3.MoveTowards(myPos, lastPlayerPosition, jumpAttackMovementSpeed * Time.deltaTime);
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
