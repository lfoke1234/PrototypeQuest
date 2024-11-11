using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    private Player player;
    private CharacterController characterController;

    [SerializeField] private Transform target;
    private Ray lastRay;

    private void Awake()
    {
    }

    private void Start()
    {
        player = GetComponent<Player>();
        AssignKey();
    }

    private void MovetoRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool hasHit = Physics.Raycast(ray, out hit);

        if (hasHit)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
        //Debug.DrawLine(lastRay.origin, lastRay.direction * 100);
    }

    private void AssignKey()
    {
        characterController = player.controller;

        characterController.Player.ClickMove.performed += ctx => { MovetoRay(); };
    }
}
