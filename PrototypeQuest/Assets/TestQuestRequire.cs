using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuestRequire : MonoBehaviour
{
    [SerializeField] private Quest test;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            QuestManager.instance.AddQuest(test);
        }
    }
}
