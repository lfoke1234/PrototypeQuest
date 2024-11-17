using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum TargetType
{
    None,
    NPC,
    Enemy,
    Object
}

public class Target : MonoBehaviour
{
    public CharacterStat stat;

    protected virtual void Start()
    {
        stat = GetComponent<CharacterStat>();
    }

    protected virtual void InteractionEvent()
    {

    }

    public virtual void Die()
    {

    }
}
