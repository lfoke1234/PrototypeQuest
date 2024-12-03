using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.DualShock;

public class Target_NPC : Target
{
    [SerializeField] protected DialogueTrigger trigger;

    [SerializeField] protected GameObject interacionText;
    [SerializeField] protected bool reverseText;
    [SerializeField] protected float interactionDistance;

    [SerializeField] protected Vector3 textOffset = new Vector3(0, 2f, 0);
    [SerializeField] private Quest quest;

    protected override void Start()
    {
        base.Start();
        if (interacionText != null)
        {
            interacionText.SetActive(false);
        }
    }

    protected virtual void Update()
    {
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        float distance = Vector3.Distance(transform.position, PlayerManager.instance.player.transform.position);
        if (distance < interactionDistance)
        {
            interacionText.SetActive(true);
            RotateText();

            if (Input.GetKeyDown(KeyCode.F))
            {
                InteractionEvent();
            }
        }
        else if (distance >= interactionDistance)
        {
            interacionText.SetActive(false);
        }
    }

    private void RotateText()
    {
        interacionText.transform.position = transform.position + textOffset;

        if (reverseText)
        {
            interacionText.transform.LookAt(2 * interacionText.transform.position - Camera.main.transform.position);

            Vector3 currentRotation = interacionText.transform.eulerAngles;
            interacionText.transform.eulerAngles = new Vector3(-currentRotation.x, currentRotation.y, currentRotation.z);
        }
        else
        {
            interacionText.transform.LookAt(2 * interacionText.transform.position - Camera.main.transform.position);
        }

        Vector3 adjustedRotation = interacionText.transform.eulerAngles;
        interacionText.transform.eulerAngles = new Vector3(adjustedRotation.x, 0, 0);
    }


    public override void InteractionEvent()
    {
        trigger.TriggerDialogue();

        if(quest != null)
            trigger.SetQuest(quest);
    }
}
