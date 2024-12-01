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

        enemy.agent.speed = 10;
        lastPlayerPosition = enemy.player.position;

        float distanceToPlayer = Vector3.Distance(lastPlayerPosition, enemy.transform.position);
        jumpAttackMovementSpeed = distanceToPlayer / enemy.timeToTarget;

        enemy.FaceTarget(lastPlayerPosition, 1000);
    }

    public override void Exit()
    {
        base.Exit();
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
            stateMachine.ChangeState(enemy.moveState);
    }
}
