using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Data/Quest/FindDaughter")]
public class Quest_FindDaughter : Quest
{
    public override bool CompletedQuest()
    {
        if (GameManager.Instance.endGame)
            return true;

        return false;
    }

    public override void StartQuest()
    {
    }

    public override void UpdateQuest()
    {
        base.UpdateQuest();
    }
}
