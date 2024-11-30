using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_QSkill : Skill
{
    [SerializeField] private GameObject qSkillPrefab;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lifeTime;
    [SerializeField] private int damage;

    public override void UseSkill()
    {
        player.animator.SetTrigger("QSkill");
    }

    public void SkillTrigger()
    {
        GameObject qSkill = ObjectPool.instance.GetObject(qSkillPrefab);

        qSkill.transform.position = player.check.qSkillChecker.position;
        qSkill.GetComponent<QSkill_Effect>().SetUpTornado(moveSpeed, damage, lifeTime);
    }
}
