using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Player player;
    private PlayerInputController playerInput;

    public bool isAttacking { get; private set; }

    // Base Attack
    private int attackCount;
    private float attackTimer;
    private float lastAttackTime;

    // E Skill

    private float radious;

    private void Start()
    {
        player = GetComponent<Player>();
        AssigneKey();
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;

        UseESkill();
    }

    private void Attack()
    {
        if (isAttacking || DialogueManager.instance.isDialgoueActive) return;

        if (lastAttackTime + 2f < Time.time || attackCount > 1)
        {
            attackCount = 0;
        }

        lastAttackTime = Time.time;

        Transform closestEnemy = FindClosestEnemy(5f);
        if (closestEnemy != null)
        {
            RotateImmediatelyTowards(closestEnemy.position);
        }

        player.animator.SetInteger("AttackCount", attackCount);
        player.animator.SetTrigger("Attack");

        SetBusyAttack(true);

        attackCount++;
    }

    public void UseESkill()
    {
        if (isAttacking || DialogueManager.instance.isDialgoueActive) return;


        if (Input.GetKeyDown(KeyCode.E) && SkillManager.instance.eSkill.CanUseSkill())
        {
            SetBusyAttack(true);
            Transform closestEnemy = FindClosestEnemy(5f);

            if (closestEnemy != null)
            {
                RotateImmediatelyTowards(closestEnemy.position);
            }

            SkillManager.instance.eSkill.UseSkill();
        }
    }


    #region Attack rogic
    private Transform FindClosestEnemy(float range)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<Target_Enemy>())
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = collider.transform;
                }
            }
        }

        return closestEnemy;
    }

    private void RotateImmediatelyTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = lookRotation;
    }
    #endregion

    public void SetBusyAttack(bool busy)
    {
        isAttacking = busy;
        player.animator.SetBool("isAttacking", isAttacking);
    }

    private void AssigneKey()
    {
        playerInput = player.playerInput;

        playerInput.Player.Attack.performed += ctx => Attack();
    }
}
