using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [Header("UI Info")]
    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> lines;
    private string currentLineText;

    [Header("Dialogue Info")]
    public bool isDialgoueActive = false;
    public float typingSpeed = 0.2f;
    public Animator animator;
    private bool isTyping = false;

    [Header("Audio")]
    public AudioSource audioSource;

    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        animator = GetComponent<Animator>();
        lines = new Queue<DialogueLine>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDialgoueActive)
        {
            HandleNextDialogue();
        }
    }

    public void HandleNextDialogue()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueArea.text = currentLineText;
            isTyping = false;
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
            DisplayNextDialogueLine();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialgoueActive = true;

        if (dialogue.quest != null)
        {
            QuestManager.instance.AddQuest(dialogue.quest);
        }

        animator.Play("Show");
        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();
        currentLineText = currentLine.line;

        characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;

        if (currentLine.clip != null)
        {
            audioSource.clip = currentLine.clip;
            audioSource.Play();
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    private IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        isTyping = true;

        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    private void EndDialogue()
    {
        isDialgoueActive = false;
        animator.Play("Hide");

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
