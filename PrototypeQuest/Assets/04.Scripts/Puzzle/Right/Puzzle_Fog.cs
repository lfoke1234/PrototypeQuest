using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Fog : MonoBehaviour
{
    [SerializeField] Transform spawnRock;
    [SerializeField] Transform platform;
    [SerializeField] Transform respawnPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TargetObject>() != null)
        {
            other.gameObject.transform.position = spawnRock.position;
            other.gameObject.transform.rotation = spawnRock.rotation;

            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (other.GetComponentInParent<Player>() != null)
        {
            PlayerManager.instance.player.playerMovement.Teleport(respawnPos.position);
        }
    }
}
