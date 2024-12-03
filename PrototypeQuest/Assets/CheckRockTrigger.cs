using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRockTrigger : MonoBehaviour
{
    [SerializeField] private Transform spawnRock;

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
    }
}
