using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_WarptoPlatform : MonoBehaviour
{
    [SerializeField] private Transform warpPoint;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>() != null)
        {
            PlayerManager.instance.player.playerMovement.Teleport(warpPoint.position);
        }
    }
}
