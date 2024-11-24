using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    private Player player;
    private Target_Enemy enemy;
    private NavMeshAgent agent;

    [Header("Track Info")]
    [SerializeField] private float chaseDistance = 5f;
    public Transform attackChecker;
    public Vector3 attackCheckSize;

    [Header("Attack Info")]
    private float lastAttackTime;
    [SerializeField] private float attackCooldown;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Target_Enemy>();

        player = PlayerManager.instance.player;
    }

    private void Update()
    {
        CheckPlayerDistance();
    }

    #region Attack
    private void CheckPlayerDistance()
    {
        if (enemy.isStunned)
            return;

        if (Vector3.Distance(player.transform.position, transform.position) < chaseDistance && player.stat.isDead == false)
        {
            agent.destination = player.transform.position;

            if (IsPlayerDetected() && lastAttackTime + attackCooldown < Time.time)
            {
                lastAttackTime = Time.time;
                enemy.animator.SetTrigger("Attack");
                agent.isStopped = true;

            }
        }
        else
        {
            agent.destination = transform.position;
        }
    }

    private bool IsPlayerDetected()
    {
        Collider[] hits = Physics.OverlapBox(attackChecker.position, attackCheckSize / 2, attackChecker.rotation);
        foreach (Collider hit in hits)
        {
            Player detectedPlayer = hit.GetComponent<Player>();
            if (detectedPlayer != null && detectedPlayer == player)
            {
                return true;
            }
        }
        return false;
    }

    private void SyncAgentAndDisableController()
    {
        agent.Warp(transform.position);
    }
    #endregion

    private void OnDrawGizmos()
    {
        if (attackChecker != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackChecker.position,attackCheckSize);
        }
    }
}
