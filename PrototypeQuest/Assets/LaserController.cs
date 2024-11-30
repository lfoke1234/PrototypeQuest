using UnityEngine;

public class LaserController : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private Transform laserStartPoint;
    [SerializeField] private Transform laserEndPoint;
    [SerializeField] private float laserDuration = 0.5f; 

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FireLaser();
        }
    }

    private void FireLaser()
    {
        lineRenderer.enabled = true;

        lineRenderer.SetPosition(0, laserStartPoint.position);
        lineRenderer.SetPosition(1, laserEndPoint.position);

        Invoke("DisableLaser", laserDuration);
    }

    private void DisableLaser()
    {
        lineRenderer.enabled = false;
    }
}
