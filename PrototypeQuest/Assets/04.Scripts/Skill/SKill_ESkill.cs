using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class SKill_ESkill : Skill
{
    [SerializeField] private GameObject eSkillPrefab;
    private IObjectPool<ESkill_Effect> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<ESkill_Effect>(CreateEffect, OnGetEffect, OnReleaseEffect, OnDestroyEffect, maxSize: 3);
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void UseSkill()
    {
        player.animator.SetTrigger("ESkill");
    }
    
    public void InstantiateSkillEffect()
    {
        var effect = _pool.Get();
        effect.transform.position = player.check.eSkillChecker.transform.position;
    }

    private ESkill_Effect CreateEffect()
    {
        ESkill_Effect effect = Instantiate(eSkillPrefab).GetComponent<ESkill_Effect>();
        effect.SetManagedPool(_pool);
        return effect;
    }

    private void OnGetEffect(ESkill_Effect effect)
    {
        effect.gameObject.SetActive(true);
    }

    private void OnReleaseEffect(ESkill_Effect effect)
    {
        effect.gameObject.SetActive(false);
    }

    private void OnDestroyEffect(ESkill_Effect effect)
    {
        Destroy(effect.gameObject);
    }
}
