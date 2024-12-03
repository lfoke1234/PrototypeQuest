using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterStat : MonoBehaviour
{
    [SerializeField] private int damage;

    [SerializeField] private int health;
    [SerializeField] private int mana;
    public int currentHealth;
    public int currentMana;

    public System.Action onHealthChanged;
    
    public bool isDead;
    private float recoverTimer = 5f;

    protected virtual void Start()
    {
        currentHealth = health;
        currentMana = mana;
    }

    private void Update()
    {
        recoverTimer -= Time.deltaTime;

        if (recoverTimer <= 0)
        {
            IncreaseHealth(3);
            recoverTimer = 0.5f;
        }
    }

    public int GetMaxHealth()
    {
        return health;
    }

    public int GetMaxMana()
    {
        return health;
    }

    public void DoDamage(CharacterStat stat)
    {
        stat.TakeDamageWithValue(damage);
    }

    public void IncreaseHealth(int amount)
    {
        currentHealth += amount;

        if (currentHealth > health)
            currentHealth = health;

        if (onHealthChanged != null)
            onHealthChanged();
    }

    public void TakeDamageWithValue(int damage)
    {
        recoverTimer = 10f;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;

            if (isDead == false)
                Die();
        }

        if (onHealthChanged != null)
            onHealthChanged();
    }

    protected virtual void Die()
    {
        isDead = true;
    }
}
