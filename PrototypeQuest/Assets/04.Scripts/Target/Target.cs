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
    public CharacterStat stat { get; private set; }
    public Animator animator { get; private set; }

    protected virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        stat = GetComponent<CharacterStat>();
    }

    public virtual void InteractionEvent()
    {

    }

    public virtual void Die()
    {

    }
}
