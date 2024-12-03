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
        GameManager.Instance.endGame2 = true;
    }

    public void GameEnd()
    {
        StartCoroutine(EndGame());
    }

    public IEnumerator EndGame()
    {
        Debug.Log("Clear Game");
        GameManager.Instance.endGame = true;
        yield return new WaitForSeconds(2f);
        Debug.Log("Thank you for playing");
        Application.Quit();
    }
}
