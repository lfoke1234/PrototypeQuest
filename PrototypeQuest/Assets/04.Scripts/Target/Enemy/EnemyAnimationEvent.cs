using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimationEvent : MonoBehaviour
{
    private AIController controller;
    private Target_Enemy enemy;
    [SerializeField] private LayerMask isPlayer;

    private void Start()
    {
        controller = GetComponentInParent<AIController>();
        enemy = GetComponentInParent<Target_Enemy>();
    }

    private void BaseAttack()
    {
        Collider[] hitColliders = Physics.OverlapBox(
            controller.attackChecker.position,
            controller.attackCheckSize / 2,
            controller.attackChecker.rotation,
            isPlayer);

        foreach (Collider collider in hitColliders)
        {
            if (collider.GetComponent<Player>() != null)
            {
                Player player = collider.GetComponent<Player>();
                enemy.stat.DoDamage(player.stat);
            }
        }
    }

    private void IsAttackOver()
    {
        GetComponentInParent<NavMeshAgent>().isStopped = false;
    }
}
