using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private Player player;
    private Animator animator;

    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float impurceForce;

    private void Start()
    {
        animator = GetComponent<Animator>();

        player = GetComponentInParent<Player>();
    }

    #region Attack
    private void IsAttackOver()
    {
        player.playerAttack.SetBusyAttack(false);
    }

    private void BaseAttack()
    {
        Collider[] hitColliders = Physics.OverlapBox(
            player.check.attackChecker.position, 
            player.check.attackCheckSize, 
            player.check.attackChecker.rotation, 
            enemyMask);

        foreach (Collider collider in hitColliders)
        {
            if (collider.GetComponentInParent<Enemy>() != null)
            {
                Vector3 forceDirection = transform.forward.normalized * impurceForce;

                // Rigidbody rb = collider.GetComponent<Rigidbody>();

                Enemy enemy = collider.GetComponentInParent<Enemy>();
                player.stat.DoDamage(enemy.stat);
                // enemy.GetHit();
                // enemy.HitImpact(forceDirection, rb);
            }
        }
    }
    #endregion

    #region Skill
    private void ESkillEvent()
    {
        SkillManager.instance.eSkill.InstantiateSkillEffect();
    }

    private void ESkillEnd()
    {
        player.playerAttack.SetBusyAttack(false);
    }

    private void QSkillEvent()
    {
        SkillManager.instance.qSkill.SkillTrigger();
    }

    private void QSkillEnd()
    {
        Debug.Log("QSkillEnd triggered!");
        player.playerAttack.SetBusyAttack(false);
    }
    #endregion
}
