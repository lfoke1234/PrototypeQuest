using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_WarptoPlatform : MonoBehaviour
{
    [SerializeField] private Transform warpPoint;
    [SerializeField] private Puzzle_Platform plarfprm;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>() != null)
        {
            PlayerManager.instance.player.playerMovement.Teleport(warpPoint.position);
            plarfprm.isPatrolling = true;
        }
    }
}
