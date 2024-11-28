using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuestRequire : MonoBehaviour
{
    [SerializeField] private Quest test;
    private bool isTrigged;
    private bool wait;

    private void Start()
    {
        Invoke("SetQuest", 0.3f);
    }

    private void SetQuest()
    {
        DialogueTrigger trigger = GetComponent<DialogueTrigger>();

        trigger.SetQuest(test);
        trigger.TriggerDialogue();

        wait = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Player>() != null && isTrigged == false && DialogueManager.instance.isDialgoueActive == false && wait == true)
        {
            isTrigged = true;
            Debug.Log("Start Tutorial");
        }
    }
        
}
