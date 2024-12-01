using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public List<GameObject> enemies;
}

public class Puzzle_Wave : MonoBehaviour
{
    [SerializeField] private List<EnemyWave> enemyWavesA;
    [SerializeField] private GameObject SealStone;

    private List<GameObject> currentListA;
    private int currentWave = 0;
    private bool isActive;
    private bool startBattle;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null && isActive == false)
        {
            
            StartWave();
            isActive = true;
        }
    }

    private void StartWave()
    {
        if (currentWave < enemyWavesA.Count)
        {
            currentListA = enemyWavesA[currentWave].enemies;
            SpawnEnemy(currentListA);
        }
    }

    private void SpawnEnemy(List<GameObject> enemyList)
    {
        foreach (GameObject enemy in enemyList)
        {
            if (enemy != null)
            {
                enemy.SetActive(true);
            }
        }
    }

    private void Update()
    {
        if (isActive)
            CheckEnemiesStatus();
    }

    private void CheckEnemiesStatus()
    {
        currentListA.RemoveAll(enemy => enemy == null || IsEnemyDead(enemy));

        if (currentListA.Count == 0 && isActive)
        {
            currentWave++;
            if (currentWave < enemyWavesA.Count)
            {
                StartWave();
            }
            else
            {
                Clear();
            }
        }
    }

    private bool IsEnemyDead(GameObject enemy)
    {
        EnemyStat stat = enemy.GetComponent<EnemyStat>();
        return stat != null && stat.isDead;
    }

    private void Clear()
    {
        SealStone.GetComponent<Target_NPC>().enabled = true;
    }
}
