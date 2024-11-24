using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Target_Enemy : Target
{
    private NavMeshAgent agent;

    private Vector3 initLocalPosition;

    public float stunnedTime;
    public bool isStunned;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        initLocalPosition = animator.transform.localPosition;
    }

    private void Update()
    {
        SetMoveAnimation();
    }

    private void LateUpdate()
    {
        animator.transform.localPosition = initLocalPosition;
    }

    private void SetMoveAnimation()
    {
        Vector3 velocity = Vector3.zero;

        velocity = agent.velocity;

        Vector3 localVelocity = transform.InverseTransformDirection(velocity);

        animator.SetFloat("forwardSpeed", localVelocity.z);
    }

    public IEnumerator Stunned(float time)
    {
        if (isStunned || stat.isDead) yield break;

        isStunned = true;

        animator.SetTrigger("Stun");
        agent.isStopped = true;

        stunnedTime = time;

        Vector3 knockbackDirection = -transform.forward;
        transform.position += knockbackDirection * 1.0f;

        yield return new WaitForSeconds(stunnedTime);

        isStunned = false;
        animator.SetTrigger("Recover");

        agent.isStopped = false;
    }



    public override void Die()
    {
        animator.SetTrigger("Die");

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
}
