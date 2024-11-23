using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ESkill_Effect : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DestroyEffect", 2f);
    }

    private IObjectPool<ESkill_Effect> _managedPool;

    public void SetManagedPool(IObjectPool<ESkill_Effect> pool)
    {
        _managedPool = pool;
    }

    private void OnTriggerEnter(Collider other)
    {
        Target target = other.GetComponent<Target>();

        if (target != null)
        {
            PlayerManager.instance.player.stat.DoDamage(target.stat);
        }
    }

    public void DestroyEffect()
    {
        _managedPool.Release(this);
    }
}
