using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ESkill_Effect : MonoBehaviour
{
    private IObjectPool<ESkill_Effect> _managedPool;
    [SerializeField] private float explotionPower;
    [SerializeField] private float explotionRaidous;

    private void OnEnable()
    {
        Invoke("DestroyEffect", 2f);
        //CheckEnemiesInRange();
        StartCoroutine(DelayedCheckEnemiesInRange());
    }


    private void CheckEnemiesInRange()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explotionRaidous);
        foreach (Collider hitCollider in hitColliders)
        {
            Enemy target = hitCollider.GetComponentInParent<Enemy>();
            if (target != null)
            {
                PlayerManager.instance.player.stat.DoDamage(target.stat);
                // target.StartCoroutine(target.Stunned(3));
                // TODO => Stun
            }

            TargetObject targetObject = hitCollider.GetComponent<TargetObject>();
            if (targetObject != null)
            {
                targetObject.ApplyExplosionForce(transform.position, explotionPower, explotionRaidous);
            }
        }
    }

    private IEnumerator DelayedCheckEnemiesInRange()
    {
        yield return new WaitForSeconds(0.1f); // 0.1초 정도 대기하여 스킬 범위가 정확하게 설정되도록 함
        CheckEnemiesInRange();
    }

    #region Pooling
    public void SetManagedPool(IObjectPool<ESkill_Effect> pool)
    {
        _managedPool = pool;
    }

    public void DestroyEffect()
    {
        _managedPool.Release(this);
    }
    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, explotionRaidous);
    }

}
