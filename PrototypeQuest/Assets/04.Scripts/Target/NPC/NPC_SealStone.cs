using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_SealStone : Target_NPC
{
    [SerializeField] private GameObject timeline;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Puzzle_CinemanchineTrigger02 vCamControl;

    public override void InteractionEvent()
    {
        timeline.SetActive(true);
    }

    public void EndEvent()
    {
        trigger.TriggerDialogue();
        vCamControl.StartCoroutine(vCamControl.SmoothRotate());
        timeline.SetActive(false);
    }

    public void TeleportPlayer()
    {
        PlayerManager.instance.player.playerMovement.Teleport(spawnPosition.position);
    }
}
