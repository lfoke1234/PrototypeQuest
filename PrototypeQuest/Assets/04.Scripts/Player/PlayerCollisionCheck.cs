using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionCheck : MonoBehaviour
{
    public Transform attackChecker;
    public Vector3 attackCheckSize;

    public Transform eSkillChecker;
    public Transform qSkillChecker;

    private void OnDrawGizmos()
    {
        // Attack Check
        Gizmos.color = Color.red;
        Gizmos.matrix = attackChecker.localToWorldMatrix;
        Gizmos.DrawWireCube(Vector3.zero, attackCheckSize);
    }
}
