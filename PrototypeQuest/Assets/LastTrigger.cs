using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastTrigger : MonoBehaviour
{
    private bool a;
    private DialogueTrigger trigger => GetComponent<DialogueTrigger>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>() != null && a == false)
        {
            a = true;
            trigger.TriggerDialogue();
        }
    }
}
