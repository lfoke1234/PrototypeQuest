using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_UnlockSkill : MonoBehaviour
{
    [SerializeField] private GameObject tutorial;
    DialogueTrigger trigger;
    bool a;
    [SerializeField] private bool b;

    private void Start()
    {
        trigger = GetComponent<DialogueTrigger>();
    }

    private void Update()
    {
        if (DialogueManager.instance.isDialgoueActive == false && a)
        {
            tutorial.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>() != null)
        {
            trigger.TriggerDialogue();
            a = true;

            if(b)
            {
                ProgressManager.instance.unlockQSkill = true;
            }
        }
    }
}
