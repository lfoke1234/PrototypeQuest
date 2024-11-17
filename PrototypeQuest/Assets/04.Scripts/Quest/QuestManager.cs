using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    // public Quest currentQuest;
    public List<Quest> activeQuests = new List<Quest>();
    public List<Quest> completedQuests = new List<Quest>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        for (int i = 0; i < activeQuests.Count; i++)
        {
            if (activeQuests[i] != null)
                activeQuests[i].UpdateQuest();

            if (activeQuests[i] != null && activeQuests[i].CompletedQuest())
            {
                CompletedQuest(activeQuests[i]);
                i--;
            }
        }
    }


    public void AddQuest(Quest quest)
    {
        if (!activeQuests.Contains(quest))
        {
            Debug.Log("Start Quest " + quest.name);
            activeQuests.Add(quest);
            quest.StartQuest();
        }
    }

    public void CompletedQuest(Quest quest)
    {
        if (activeQuests.Contains(quest))
        {
            activeQuests.Remove(quest);
            completedQuests.Add(quest);
            Debug.Log($"Quest completed: {quest.questName}");
        }
    }

    // private void StartQuest() => currentQuest.StartQuest();

    // public bool CompletedQuest() => currentQuest.CompletedQuest();
}
