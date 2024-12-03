using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public Quest currentQuest;

    [Header("UI Info")]
    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    public Queue<DialogueLine> lines;
    private string currentLineText;

    [Header("Dialogue Info")]
    public bool isDialgoueActive = false;
    public float typingSpeed = 0.2f;
    public Animator animator;
    private bool isTyping = false;

    [Header("Audio")]
    public AudioSource audioSource;

    [SerializeField] private Image image;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
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

        animator.Play("Show");
        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();

        GameManager.Instance.ingameUI.DisbleInGameUI();
        SetAllRaycastTargets(true);
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

        if (currentQuest != null)
        {
            Debug.Log("Add Quest " + currentQuest.name);
            QuestManager.instance.AddQuest(currentQuest);
            currentQuest = null;
        }

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        if (GameManager.Instance.endGame2)
        {
            FindObjectOfType<Endging>().GameEnd();
            GameManager.Instance.endGame = true;
        }

        if(GameManager.Instance.endGame == false)
        {
            SetAllRaycastTargets(false);
            GameManager.Instance.ingameUI.EnableInGameUI();
        }
    }

    private void SetAllRaycastTargets(bool state)
    {
        image.raycastTarget = state;
    }
}
