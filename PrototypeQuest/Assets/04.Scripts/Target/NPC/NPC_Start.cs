using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Start : Target_NPC
{
    [SerializeField] private GameObject gameob;

    public override void InteractionEvent()
    {
        base.InteractionEvent();
        gameob.SetActive(false);
    }
}
