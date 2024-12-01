using System.Collections.Generic;
using UnityEngine;

public class Puzzle_PlatformSync : MonoBehaviour
{
    private Vector3 lastPosition;
    private List<Transform> passengers = new List<Transform>();
    private bool isTriggered = false;

    private Puzzle_Platform platform;

    private void Start()
    {
        platform = GetComponent<Puzzle_Platform>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        bool isPatrolling = platform.isPatrolling;

        if (isPatrolling && !isTriggered)
        {
            isTriggered = true;
            passengers.Add(PlayerManager.instance.player.transform);
        }

        if (!isPatrolling && isTriggered)
        {
            isTriggered = false;
            passengers.Remove(PlayerManager.instance.player.transform);
        }

        Vector3 movementDelta = transform.position - lastPosition;

        foreach (Transform passenger in passengers)
        {
            passenger.position += movementDelta;
        }

        lastPosition = transform.position;
    }
}
