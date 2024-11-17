using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Quest : ScriptableObject
{
    public string questName;

    [TextArea]
    public string questDescription;

    public abstract void StartQuest();

    public abstract bool CompletedQuest();

    public virtual void UpdateQuest()
    {

    }
}
