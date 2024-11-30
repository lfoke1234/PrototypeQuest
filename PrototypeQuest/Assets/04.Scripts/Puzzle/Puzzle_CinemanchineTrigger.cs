using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_CinemanchineTrigger : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Vector3 targetRotationEuler;
    [SerializeField] private float rotationSpeed;

    private DialogueTrigger trigger;
    private bool next;

    private void Start()
    {
        trigger = GetComponent<DialogueTrigger>();
        virtualCamera = GameObject.Find("VC_PlayerFollower").GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            if (next == false)
            {
                trigger.TriggerDialogue();
                next = true;
            }

            if (DialogueManager.instance.isDialgoueActive == false && next)
                StartCoroutine(RotationVC());
        }
    }

    private IEnumerator RotationVC()
    {
        Transform cameraTransform = virtualCamera.transform;
        Quaternion startRotation = cameraTransform.rotation;
        Quaternion targetRotation = Quaternion.Euler(targetRotationEuler);

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * rotationSpeed;

            cameraTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime);

            yield return null;
        }

        cameraTransform.rotation = targetRotation;
    }
}