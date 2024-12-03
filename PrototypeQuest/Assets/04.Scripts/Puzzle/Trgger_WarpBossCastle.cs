using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trgger_WarpBossCastle : MonoBehaviour
{
    [SerializeField] private Transform warpPoint;
    [SerializeField] private GameObject timeline;
    [SerializeField] private GameObject boss;
    [SerializeField] private GameObject[] timelineObjects;
    [SerializeField] private GameObject vCam;
    DialogueTrigger trigger => GetComponent<DialogueTrigger>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>() != null)
        {
            PlayerManager.instance.player.animator.SetFloat("forwardSpeed", 0);
            timeline.SetActive(true);
        }
    }

    public void SetBossFight()
    {
        PlayerManager.instance.player.playerMovement.Teleport(warpPoint.position);
        boss.gameObject.SetActive(true);
        boss.GetComponent<Enemy_Boss>().dontMove = true;

        foreach (GameObject obj in timelineObjects)
        {
            obj.SetActive(false);
        }
    }

    public void LastScene()
    {
        boss.GetComponent<Enemy_Boss>().dontMove = false;
        timeline.SetActive(false);
        vCam.SetActive(false);
        trigger.TriggerDialogue();
    }
}
