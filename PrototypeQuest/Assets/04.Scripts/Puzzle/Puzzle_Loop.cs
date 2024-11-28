using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Loop : MonoBehaviour
{
    private List<int> loopSequence = new List<int>();
    private List<int> playerSequence = new List<int>();

    private int loopCount = 0;
    public bool startLoop { get; private set; }
    private bool firstLoop;

    private DialogueTrigger trigger01;
    private Quest quest;

    [SerializeField] private GameObject clue;

    private void Start()
    {
        trigger01 = GetComponent<DialogueTrigger>();

        loopSequence.Add(0);
        loopSequence.Add(0);
        loopSequence.Add(1);
    }

    private void Update()
    {
        if (startLoop == false)
            return;

        if (loopCount == 1 && firstLoop == false)
        {
            firstLoop = true;
            StartCoroutine(FistLoop());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (startLoop == false && other.GetComponentInParent<Player>() != null)
        {
            startLoop = true;
        }
    }

    private IEnumerator FistLoop()
    {
        yield return new WaitForSeconds(1f);
        trigger01.TriggerDialogue();
        trigger01.SetQuest(quest);
    }

    public void ProcessLoop(int loopPoint)
    {
        playerSequence.Add(loopPoint);

        if (!CheckSequence())
        {
            playerSequence.Clear();
        }

        if (playerSequence.Count == loopSequence.Count && CheckSequence())
        {
            gameObject.SetActive(false);
            clue.SetActive(false);
            playerSequence.Clear();
        }
    }

    private bool CheckSequence()
    {
        for (int i = 0; i < playerSequence.Count; i++)
        {
            if (playerSequence[i] != loopSequence[i])
            {
                Debug.Log("false");
                return false;
            }
        }
        Debug.Log("true");
        return true;
    }

    public void IncreaseLoopCount()
    {
        loopCount++;
    }

}
