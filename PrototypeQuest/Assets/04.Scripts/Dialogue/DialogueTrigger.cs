using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialogueLine
{
    public AudioClip clip;
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public Quest quest;
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}


public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialouge;

    public void TriggerDialogue()
    {
        DialogueManager.instance.StartDialogue(dialouge);
    }

    public void SetQuest(Quest quest)
    {
        DialogueManager.instance.currentQuest = quest;
    }
}
