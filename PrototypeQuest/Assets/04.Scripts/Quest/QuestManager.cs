using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    // public Quest currentQuest;
    public List<Quest> activeQuests = new List<Quest>();
    public List<Quest> completedQuests = new List<Quest>();

    [SerializeField] private UI_Quest startQuestUI;

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
            activeQuests.Add(quest);
            quest.StartQuest();

            startQuestUI.gameObject.SetActive(true);
            startQuestUI.SetQuestInfo(quest, true);
        }
    }

    public void CompletedQuest(Quest quest)
    {
        if (activeQuests.Contains(quest))
        {
            activeQuests.Remove(quest);
            completedQuests.Add(quest);

            startQuestUI.gameObject.SetActive(true);
            startQuestUI.SetQuestInfo(quest, false);
        }
    }

    // private void StartQuest() => currentQuest.StartQuest();

    // public bool CompletedQuest() => currentQuest.CompletedQuest();
}
