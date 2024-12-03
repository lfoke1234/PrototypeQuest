using UnityEngine;

public class ThrowState_Melee : EnemyState
{
    private Enemy_Melee enemy;

    public ThrowState_Melee(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        enemy = enemyBase as Enemy_Melee;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.ShowWeapon();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.animator.SetFloat("RecoveryIndex", 1);
    }

    public override void Update()
    {
        base.Update();

        if (enemy.ManualRotationActive())
        {
            enemy.transform.rotation = enemy.FaceTarget(enemy.player.position);
        }

        if (triggerCalled)
            stateMachine.ChangeState(enemy.recoveryState);
    }

    public override void SkillTrigger()
    {
        base.SkillTrigger();

        GameObject Knife = ObjectPool.instance.GetObject(enemy.throwKnifePrefab);

        Knife.transform.position = enemy.throwPoint.position;
        Knife.GetComponent<EnemyThrowKnife>().KnifeSetup(enemy.throwKnifeSpeed, enemy.player, enemy.throwKnifeTimer);
    }
}
