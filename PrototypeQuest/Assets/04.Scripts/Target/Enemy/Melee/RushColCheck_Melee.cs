using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RushColCheck_Melee : MonoBehaviour
{
    private bool doDamaged;

    EnemyStat stat;

    private void Start()
    {
        stat = GetComponentInParent<EnemyStat>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>() != null && doDamaged == false)
        {
            doDamaged = true;
            stat.DoDamage(other.GetComponentInParent<Player>().stat);
        }
    }

    private void OnDisable()
    {
        doDamaged = false;
    }
}
