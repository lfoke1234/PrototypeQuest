using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    private Enemy enemy;
    public Enemy_Boss enemyBoss;
    public Enemy_Melee enemyMelee;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void AnimationTrigger() => enemy.AnimationTrigger();

    public void StartManualMovement() => enemy.ActiveManualMovemet(true);
    public void StopManualMovement() => enemy.ActiveManualMovemet(false);

    public void StartManualRotation() => enemy.ActiveManualRotation(true);
    public void StopManualRotation() => enemy.ActiveManualRotation(false);

    public void ThrowEvent() => enemy.SkillTrigger();

    public void BossJumpInpact()
    {
        enemyBoss.JumpImpact();
    }

    public void MeleeAttack()
    {
        Collider[] hitColliders = Physics.OverlapBox(enemy.attackCheck.position, enemy.attackSize / 2, Quaternion.identity);
        foreach (Collider collider in hitColliders)
        {
            if (collider.GetComponentInParent<Player>() != null)
            {
                Player player = collider.GetComponentInParent<Player>();
                enemy.stat.DoDamage(player.stat);
            }
        }
    }

    public void RushStart()
    {
        if (enemyMelee == null)
            enemyMelee = GetComponentInParent<Enemy_Melee>();

        enemyMelee.EnableRushCol();
    }

    public void RushEnd()
    {
        if (enemyMelee == null)
            enemyMelee = GetComponentInParent<Enemy_Melee>();

        enemyMelee.DisableRushCol();
    }
}
