using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Target_Enemy : Target
{
    public override void Die()
    {
        for (int i = 0; i < QuestManager.instance.activeQuests.Count; i++)
        {
            Quest quest = QuestManager.instance.activeQuests[i];

            if (quest is Quest_KillTarget killQuest && killQuest.targetType == TargetType.Enemy)
            {
                killQuest.amountToKill++;

                if (killQuest.CompletedQuest())
                {
                    QuestManager.instance.CompletedQuest(killQuest);
                    i--;
                }
            }
        }
    }


}
