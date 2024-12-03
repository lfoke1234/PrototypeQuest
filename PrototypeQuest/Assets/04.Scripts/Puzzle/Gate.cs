using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private Animator anim;

    public bool clearRightPuzzle;
    public bool clearLeftPuzzle;

    [SerializeField] private GameObject rightObject;
    [SerializeField] private GameObject leftObject;
    [SerializeField] private Material clearMaterial;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ClearRigthPuzzle() => clearRightPuzzle = true;
    public void ClearLeftPuzzle() => clearLeftPuzzle = true;

    private void Update()
    {
        if (clearLeftPuzzle)
        {
            leftObject.GetComponentInChildren<ParticleSystem>().Stop();

            Renderer leftRenderer = leftObject.GetComponentInChildren<GateSphere>().GetComponent<Renderer>();
            if (leftRenderer != null)
            {
                leftRenderer.material = clearMaterial;
            }
        }

        if (clearRightPuzzle)
        {
            rightObject.GetComponentInChildren<ParticleSystem>().Stop();

            Renderer RightRenderer = rightObject.GetComponentInChildren<GateSphere>().GetComponent<Renderer>();
            if (RightRenderer != null)
            {
                RightRenderer.material = clearMaterial;
            }
        }

        if (clearLeftPuzzle && clearRightPuzzle)
        {
            GetComponent<BoxCollider>().enabled = false;
            anim.SetTrigger("Open");
        }
    }
}
