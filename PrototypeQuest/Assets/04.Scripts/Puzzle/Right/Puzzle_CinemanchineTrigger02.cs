using Cinemachine;
using System.Collections;
using UnityEngine;

public class Puzzle_CinemanchineTrigger02 : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Vector3 targetRotationEuler;
    [SerializeField] private float rotationSpeed = 2f;

    private void Start()
    {
        virtualCamera = GameObject.Find("VC_PlayerFollower").GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<Player>() != null)
        {
            StartCoroutine(SmoothRotate());
        }
    }

    private IEnumerator SmoothRotate()
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
