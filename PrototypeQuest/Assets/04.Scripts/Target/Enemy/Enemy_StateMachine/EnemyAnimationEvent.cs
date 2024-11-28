using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvent : MonoBehaviour
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void AnimationTrigger() => enemy.AnimationTrigger();

    public void StartManualMovement() => enemy.ActiveManualMovemet(true);
    public void StopManualMovement() => enemy.ActiveManualMovemet(false);

    public void StartManualRotation() => enemy.ActiveManualRotation(true);
    public void StopManualRotation() => enemy.ActiveManualRotation(false);

}
