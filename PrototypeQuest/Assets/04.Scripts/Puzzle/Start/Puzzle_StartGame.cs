using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Puzzle_StartGame : MonoBehaviour
{
    private DialogueTrigger trigger;
    private bool s;
    private bool d;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Quest quest;

    // Start is called before the first frame update
    void Start()
    {
        trigger = GetComponent<DialogueTrigger>();
        StartCoroutine(StartGame());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponentInParent<Player>() != null &&
            DialogueManager.instance.isDialgoueActive == false && s && d == false)
        {
            d = true;
            StartCoroutine(ActiveCanvas());
        }
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5f);
        trigger.TriggerDialogue();
        s = true;
    }

    private IEnumerator ActiveCanvas()
    {
        yield return new WaitForSeconds(0.5f);
        canvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DisableCanvas()
    {
        canvas.SetActive(false);
        Time.timeScale = 1f;
        QuestManager.instance.AddQuest(quest);
    }
}