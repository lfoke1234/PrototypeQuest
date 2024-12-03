using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuestInfo : MonoBehaviour
{
    [SerializeField] private Image backGround;
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questDescription;

    public Quest currentQuest;

    public void UpdateSlot(Quest quest)
    {
        currentQuest = quest;

        questName.text = quest.questName;
        questDescription.text = quest.questDescription;
        backGround.color = new Color(0, 0, 0, 200f / 255f);
    }

    public void CleanUpSlot()
    {
        currentQuest = null;

        questName.text = "";
        questDescription.text = "";
        backGround.color = Color.clear;
    }
}
