using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_StartActive : MonoBehaviour
{
    [SerializeField] private Enemy_Melee[] enemies;

    void Update()
    {
        bool allEnemiesDead = true;
        foreach (var enemy in enemies)
        {
            if (enemy != null && !enemy.stat.isDead)
            {
                allEnemiesDead = false;
                break;
            }
        }

        if (allEnemiesDead)
        {
            gameObject.SetActive(false);
        }
    }
}
