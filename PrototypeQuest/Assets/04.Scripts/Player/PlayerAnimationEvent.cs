using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private PlayerAttack attack;

    void Start()
    {
        attack = GetComponentInParent<PlayerAttack>();
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.GetComponent<Target_Enemy>() != null)
        {

        }
    }

    private void IsAttackOver()
    {
        attack.SetBusyAttack(false);
    }

    private void AttackStart()
    {
        GetComponent<BoxCollider>().enabled = true; 
    }

    private void AttackEnd()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
}
