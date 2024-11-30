using System.Collections;
using UnityEngine;

public class QSkill_Effect : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float damageInterval = 1f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float lifetime = 10f;
    [SerializeField] private float damageRadius = 5f;

    private Transform target;
    private float damageTimer;

    private void Update()
    {
        damageTimer -= Time.deltaTime;
        target = FindClosestEnemy();

        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }

        if (damageTimer <= 0f)
        {
            ApplyDamage();
            damageTimer = damageInterval;
        }
    }

    public void SetUpTornado(float moveSpeed, int damage, float lifetime)
    {
        this.moveSpeed = moveSpeed;
        this.damage = damage;
        this.lifetime = lifetime;

        StartCoroutine(ReturnTornadoAfterLifetime());
    }

    private Transform FindClosestEnemy()
    {
        float closestDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }

    private void ApplyDamage()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemyObj in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemyObj.transform.position);

            if (distance <= damageRadius)
            {
                Enemy enemy = enemyObj.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.stat.TakeDamageWithValue(damage);
                }
            }
        }
    }

    private IEnumerator ReturnTornadoAfterLifetime()
    {
        yield return new WaitForSeconds(lifetime);
        ObjectPool.instance.ReturnObject(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}
