using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private Player player;
    [SerializeField] private LayerMask enemyMask;
    private Animator animator;

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
            if (collider.GetComponent<Target_Enemy>() != null)
            {
                Target_Enemy enemy = collider.GetComponent<Target_Enemy>();
                player.stat.DoDamage(enemy.stat);
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
    #endregion
}
