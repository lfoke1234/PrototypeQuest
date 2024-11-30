using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    private Rigidbody rb;
    private bool isDestroyObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ApplyExplosionForce(Vector3 explosionOrigin, float explosionPower, float explosionRadius)
    {
        if (rb != null)
        {
            rb.AddExplosionForce(explosionPower, explosionOrigin, explosionRadius, 2f, ForceMode.Impulse);
            if(isDestroyObject)
                Destroy(gameObject, 2f);
        }
    }
}