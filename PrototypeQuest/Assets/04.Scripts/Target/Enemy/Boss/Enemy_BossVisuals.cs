using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BossVisuals : MonoBehaviour
{
    private Enemy_Boss enemy;

    [SerializeField] private float landingOffset = 1;
    public ParticleSystem landindZoneFx;

    private void Awake()
    {
        enemy = GetComponent<Enemy_Boss>();

        landindZoneFx.transform.parent = null;
        landindZoneFx.Stop();
    }

    public void PlaceLandindZone(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        Vector3 offset = dir.normalized * landingOffset;
        landindZoneFx.transform.position = target + offset;
        landindZoneFx.Clear();

        var mainModule = landindZoneFx.main;
        mainModule.startLifetime = enemy.timeToTarget * 2;

        landindZoneFx.Play();
    }
}
