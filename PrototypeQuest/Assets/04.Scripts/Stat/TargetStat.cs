public class TargetStat : CharacterStat
{
    private Target target;

    protected override void Start()
    {
        base.Start();
        target = GetComponent<Target>();
    }

    protected override void Die()
    {
        base.Die();
        target.Die();
    }
}