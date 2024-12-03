using System.Collections;
using UnityEngine;

public class FlameParticle : MonoBehaviour
{
    [SerializeField] private int damageAmount = 5;
    [SerializeField] private float cooldownTime = 0.5f;

    private bool canDealDamage = true;

    private void OnParticleCollision(GameObject other)
    {
        if (canDealDamage && other.GetComponentInParent<Player>() != null)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.stat.TakeDamageWithValue(damageAmount);
                StartCoroutine(DamageCooldown());
            }
        }
    }

    private IEnumerator DamageCooldown()
    {
        canDealDamage = false;
        yield return new WaitForSeconds(cooldownTime);
        canDealDamage = true;
    }
}