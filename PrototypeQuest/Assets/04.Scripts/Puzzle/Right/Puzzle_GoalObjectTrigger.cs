using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle_GoalObjectTrigger : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject[] triggers;
    private CinemachineVirtualCamera virtualCamera;

    private void Start()
    {
        virtualCamera = GameObject.Find("VC_PlayerFollower").GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TargetObject>() != null)
        {
            platform.SetActive(true);
            DisableandRotateCamera();
            other.gameObject.SetActive(false);
        }
    }

    private void DisableandRotateCamera()
    {
        Goal();

        foreach (GameObject trigger in triggers)
        {
            trigger.gameObject.SetActive(false);
        }
    }

    private void Goal()
    {
        StartCoroutine(SmoothRotate());
    }

    private IEnumerator SmoothRotate()
    {
        Transform cameraTransform = virtualCamera.transform;
        Quaternion startRotation = cameraTransform.rotation;
        Quaternion targetRotation = Quaternion.Euler(65, 0, 0);

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime * 2.5f;

            cameraTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime);

            yield return null;
        }

        cameraTransform.rotation = targetRotation;
    }
}
