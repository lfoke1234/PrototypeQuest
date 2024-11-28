using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_NPC : Target
{
    [SerializeField] private DialogueTrigger trigger;

    private void Update()
    {
        if (Vector3.Distance(transform.position, PlayerManager.instance.player.transform.position) < 2f &&
            Input.GetKeyDown(KeyCode.F))
        {
            InteractionEvent();
        }
    }

    public override void InteractionEvent()
    {
        ProgressManager.instance.unlockESkill = true;
        trigger.TriggerDialogue();
    }
}
