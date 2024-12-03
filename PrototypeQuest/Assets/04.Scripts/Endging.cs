using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endging : MonoBehaviour
{
    [SerializeField] private GameObject darkScreen;
    [SerializeField] private DialogueTrigger tirgger;

    public void TriggerDialogue()
    {
        darkScreen.SetActive(true);
        tirgger.TriggerDialogue();
    }
}
