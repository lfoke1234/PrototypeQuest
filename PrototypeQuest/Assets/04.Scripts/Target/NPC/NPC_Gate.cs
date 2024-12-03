using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Gate : Target_NPC
{
    private bool trigged;
    [SerializeField]private GameObject timeline;
    [SerializeField]private DialogueTrigger trigger02;
    private float timer;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        timer -= Time.deltaTime;

        if(DialogueManager.instance.isDialgoueActive == false && trigged )
        {
            timeline.SetActive(true);
        }
    }

    public override void InteractionEvent()
    {
        base.InteractionEvent();
        trigged = true;
        timer = 0.5f;
    }

    public void TimelineEvent()
    {
        trigger02.TriggerDialogue();
        GetComponent<NPC_Gate>().enabled = false;
    }
}
