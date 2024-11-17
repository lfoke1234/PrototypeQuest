using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    [SerializeField] private int damage;

    [SerializeField] private int health;
    [SerializeField] private int currentHealth;

    public bool isDead;

    protected virtual void Start()
    {
        currentHealth = health;
    }

    public int GetMaxHealth()
    {
        return health;
    }

    public void DoDamage(CharacterStat stat)
    {
        stat.TakeDamageWithValue(damage);
    }

    public void TakeDamageWithValue(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0 && isDead == false)
        {
            currentHealth = 0;
            Die();
        }
    }

    protected virtual void Die()
    {
        isDead = true;
    }
}
