using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Start : Target_NPC
{
    [SerializeField] private Quest quest;

    public override void InteractionEvent()
    {
        ProgressManager.instance.unlockESkill = true;
        // => E Skill icon Active
        trigger.TriggerDialogue();
        trigger.SetQuest(quest);
    }
}
