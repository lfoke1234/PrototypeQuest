using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState_Melee : EnemyState
{
    private Enemy_Melee enemy;
    private Enemy_Ragdoll ragdoll;

    private bool interactionDisble;

    public DeadState_Melee(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy = enemyBase as Enemy_Melee;
        ragdoll = enemy.GetComponent<Enemy_Ragdoll>();
    }

    public override void Enter()
    {
        base.Enter();
        interactionDisble = false;

        enemy.animator.enabled = false;
        enemy.agent.isStopped = true;

        ragdoll.RagdollActive(true);
        stateTimer = 1.5f;

        for (int i = 0; i < QuestManager.instance.activeQuests.Count; i++)
        {
            Quest quest = QuestManager.instance.activeQuests[i];

            if (quest is Quest_KillTarget killQuest && killQuest.targetType == TargetType.Enemy)
            {
                killQuest.amountToKill++;

                if (killQuest.CompletedQuest())
                {
                    QuestManager.instance.CompletedQuest(killQuest);
                    i--;
                }
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0 && !interactionDisble)
        {
            interactionDisble = true;
            ragdoll.RagdollActive(false);
            ragdoll.CollidersActive(false);
        }
    }
}
