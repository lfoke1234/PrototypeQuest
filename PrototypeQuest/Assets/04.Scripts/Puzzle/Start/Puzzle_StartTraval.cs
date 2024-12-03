using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_StartTraval : MonoBehaviour
{
    [SerializeField] private GameObject timeline;
    [SerializeField]private DialogueTrigger trigger01;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>() != null)
            timeline.SetActive(true);

        GameManager.Instance.joystick.ResetJoystick();
        PlayerManager.instance.player.animator.SetFloat("forwardSpeed", 0);
    }

    public void TriggerDialogue01()
    {
        trigger01.TriggerDialogue();
        timeline.SetActive(false);
        gameObject.SetActive(false);
    }

}
