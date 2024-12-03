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

    public void UnlockESkill()
    {
        unlockESkill = true;
    }

    public void UnlockQSkill()
    {
        unlockQSkill = true;
    }

    public void Unlock()
    {
        UnlockESkill();
        UnlockQSkill();
    }
}
