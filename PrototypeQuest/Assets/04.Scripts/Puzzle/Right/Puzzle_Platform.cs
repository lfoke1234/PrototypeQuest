using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_Platform : MonoBehaviour
{
    [Header("Platform Patrol Settings")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private Transform patrolParent;
    [SerializeField] private float platformSpeed;

    [Header("Knife Spawn Settings")]
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private float spawnRadius;
    [SerializeField] private float knifeSpawnMinDelay;
    [SerializeField] private float knifeSpawnMaxDelay;
    [SerializeField] private float knifeOffsetY;
    [SerializeField] private float knifeSpeed;
    [SerializeField] private float knifeLifeTime;

    [SerializeField] private Transform spawnPoint;

    private int currentPatrolIndex = 0;
    public bool isPatrolling;
    [SerializeField] private bool trigged;

    private void Start()
    {
        patrolParent.parent = null;
    }

    private void Update()
    {
        if (isPatrolling && trigged == false)
        {
            StartPuzzle();
            trigged = true;
        }
    }

    private void StartPuzzle()
    {
        PlayerManager.instance.player.transform.SetParent(transform);
        StartCoroutine(Patrol());
        StartCoroutine(SpawnKnives());
    }

    private void EndPuzzle()
    {
        PlayerManager.instance.player.transform.SetParent(null);
        StopAllCoroutines();
        PlayerManager.instance.player.playerMovement.Teleport(spawnPoint.position);
    }

    private IEnumerator Patrol()
    {
        while (isPatrolling)
        {
            Transform targetPoint = patrolPoints[currentPatrolIndex];

            while (Vector3.Distance(transform.position, targetPoint.position) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetPoint.position,
                    platformSpeed * Time.deltaTime
                );
                yield return null;
            }

            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;

            //yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator SpawnKnives()
    {
        while (isPatrolling)
        {
            float spawnDelay = Random.Range(knifeSpawnMinDelay, knifeSpawnMaxDelay);
            yield return new WaitForSeconds(spawnDelay);

            GameObject Knife = ObjectPool.instance.GetObject(knifePrefab);

            Vector3 knifeSpawnPosition = GetRandomKnifeSpawnPosition();

            Knife.transform.position = knifeSpawnPosition;
            Knife.GetComponent<EnemyThrowKnife>().KnifeSetup(knifeSpeed, PlayerManager.instance.player.transform, knifeLifeTime);

            Knife.transform.parent = transform;
        }
    }

    private Vector3 GetRandomKnifeSpawnPosition()
    {
        float angle = Random.Range(0f, 360f);

        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * spawnRadius + transform.position.x;
        float z = Mathf.Sin(angle * Mathf.Deg2Rad) * spawnRadius + transform.position.z;

        return new Vector3(x, transform.position.y + knifeOffsetY, z);
    }

    public void StopPatrol()
    {
        isPatrolling = false;
    }
}
