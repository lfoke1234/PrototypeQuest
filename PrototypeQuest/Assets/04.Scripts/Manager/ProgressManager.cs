using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager instance;

    public bool unlockESkill;
    public bool unlockQSkill;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            Unlock();
    }

    public void Unlock()
    {
        unlockESkill = true;
        unlockQSkill = true;
    }
}
