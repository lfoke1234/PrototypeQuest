using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UI_IngameUI ingameUI;

    private void Awake()
    {
        Instance = this;
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
