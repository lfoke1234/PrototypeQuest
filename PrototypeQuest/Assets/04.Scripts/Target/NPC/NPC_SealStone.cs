using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_SealStone : Target_NPC
{
    [SerializeField] private GameObject timeline;
    public override void InteractionEvent()
    {
        timeline.SetActive(true);
    }

    public void EndEvent()
    {
        trigger.TriggerDialogue();
        timeline.SetActive(false);
    }
}
