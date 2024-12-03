using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Data/Quest/OpenDoor")]
public class Quest_OpenDoor : Quest
{
    Gate gate;

    public override bool CompletedQuest()
    {
        if (gate.clearLeftPuzzle && gate.clearRightPuzzle)
            return true;

        else return false;
    }

    public override void StartQuest()
    {
        gate = FindObjectOfType<Gate>();
    }
}
