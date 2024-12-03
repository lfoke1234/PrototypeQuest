using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public JoystickHandle joystick;

    public UI_IngameUI ingameUI;

    public bool endGame;
    public bool endGame2;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
    }

    public bool isPlayCutScene { get; private set; }

    public void StartCutScene()
    {
        isPlayCutScene = true;
        ingameUI.DisbleInGameUI();
    }

    public void EndCutScene()
    {
        isPlayCutScene = false;
        ingameUI.EnableInGameUI();
    }
}
