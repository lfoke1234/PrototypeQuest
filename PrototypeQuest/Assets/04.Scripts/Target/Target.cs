using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType
{
    None,
    NPC,
    Enemy,
    Object
}

public class Target : MonoBehaviour
{
    private int currentHealth;

    protected virtual void InteractionEvent()
    {

    }

    public void DecreaseHealthWithValue(int value) => currentHealth -= value;
    public void IncreaseHealthWithValue(int value) => currentHealth += value;
}
