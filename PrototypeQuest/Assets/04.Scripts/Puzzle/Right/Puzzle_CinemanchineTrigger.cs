using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_CinemanchineTrigger : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Vector3 targetRotationEuler;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject cameraTrigger;

    private DialogueTrigger trigger;
    private bool next;

    [SerializeField] private DialogueTrigger dialogueTrigger;
    private bool hasTriggered = false;
    private bool isRotationComplete = false;

    private void Start()
    {
        virtualCamera = GameObject.Find("VC_PlayerFollower").GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>() != null && !hasTriggered)
        {
            hasTriggered = true;
            dialogueTrigger.TriggerDialogue();
            cameraTrigger.SetActive(true);

            if (dialogueTrigger != null)
                StartCoroutine(HandleDialogueWithRotation());
        }
    }

    private IEnumerator HandleDialogueWithRotation()
    {
        while (DialogueManager.instance.isDialgoueActive)
        {
            DialogueLine currentLine = dialogueTrigger.dialouge.dialogueLines[DialogueManager.instance.lines.Count];

            if (currentLine.executeMethod && !isRotationComplete)
            {
                Invoke(currentLine.methodName, 0f);
            }

            yield return null;
        }
    }

    private void RotationVC()
    {
        StartCoroutine(RotationCoroutine());
    }

    private IEnumerator RotationCoroutine()
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
        isRotationComplete = true;
    }
}
