using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_UnlockSkill : MonoBehaviour
{
    [SerializeField] private GameObject tutorial;
    DialogueTrigger trigger;
    bool a;
    [SerializeField] private bool q;

    private void Start()
    {
        trigger = GetComponent<DialogueTrigger>();
    }

    private void Update()
    {
        if (DialogueManager.instance.isDialgoueActive == false && a)
        {
            if (q)
                ProgressManager.instance.unlockQSkill = true;
            else
                ProgressManager.instance.unlockESkill = true;

            tutorial.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>() != null && a == false)
        {
            trigger.TriggerDialogue();
            a = true;
        }
    }
}
