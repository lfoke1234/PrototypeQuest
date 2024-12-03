using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public bool isPlayCutScene { get; private set; }

    public void StartCutScene() => isPlayCutScene = true;
    public void EndCutScene() => isPlayCutScene = false;
}
