using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialActive : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerManager.instance.player.playerAttack.SetBusyAttack(true);
        Time.timeScale = 0.0f;
    }

    private void OnDisable()
    {
        PlayerManager.instance.player.playerAttack.SetBusyAttack(false);
        Time.timeScale = 1.0f;
    }
}
