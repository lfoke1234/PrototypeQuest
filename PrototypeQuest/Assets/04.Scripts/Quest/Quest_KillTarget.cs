using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Data/Quest/Kill")]
public class Quest_KillTarget : Quest
{
    public int amountToKill;
    public TargetType targetType;

    public override void StartQuest()
    {
        amountToKill = 0;   
    }

    public override bool CompletedQuest()
    {
        if (amountToKill >= 3)
        {

            return true;
        }

        return false;
    }

    public override void UpdateQuest()
    {
        base.UpdateQuest();
    }
}
