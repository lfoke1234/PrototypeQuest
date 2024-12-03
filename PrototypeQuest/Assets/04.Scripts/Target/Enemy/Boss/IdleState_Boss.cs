using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState_Boss : EnemyState
{
    Enemy_Boss enemy;

    private bool fast;

    public IdleState_Boss(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy = enemyBase as Enemy_Boss;
    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 5f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.PlayerInAttackRange() && GameManager.Instance.isPlayCutScene == false && DialogueManager.instance.isDialgoueActive == false)
        {
            stateMachine.ChangeState(enemy.attackState);
        }

        if (stateTimer <= 0 && GameManager.Instance.isPlayCutScene == false && DialogueManager.instance.isDialgoueActive == false)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
