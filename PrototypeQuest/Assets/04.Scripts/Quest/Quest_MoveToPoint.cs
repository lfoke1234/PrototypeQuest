using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Data/Quest/Time")]
public class Quest_MoveToPoint : Quest
{
    private float timer;

    public override bool CompletedQuest()
    {
        if (timer <= 0)
            return true;

        return false;
    }

    public override void StartQuest()
    {
        timer = 1f;
    }

    public override void UpdateQuest()
    {
        base.UpdateQuest();
        timer -= Time.deltaTime;
    }
}
