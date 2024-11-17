using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_NPC : Target
{
    [SerializeField] private DialogueTrigger trigger;

    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerManager.instance.player.transform.position) < 2f &&
            Input.GetKeyDown(KeyCode.E))
        {
            InteractionEvent();
        }
    }

    protected override void InteractionEvent()
    {
        trigger.TriggerDialogue();
    }
}
