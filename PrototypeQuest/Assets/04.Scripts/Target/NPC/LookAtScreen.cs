using UnityEngine;

public class LookAtScreen : MonoBehaviour
{
    private Transform mLookAt;
    private Transform localTrans;

    private void Start()
    {
        localTrans = GetComponent<Transform>();
        mLookAt = Camera.main.transform;
    }

    private void Update()
    {
        if (mLookAt)
        {
            localTrans.LookAt(2 * localTrans.position - mLookAt.position);

            Vector3 currentRotation = localTrans.eulerAngles;
            localTrans.eulerAngles = new Vector3(currentRotation.x, 0, 0);
        }
    }
}
