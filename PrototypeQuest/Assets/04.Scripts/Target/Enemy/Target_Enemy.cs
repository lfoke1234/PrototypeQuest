using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Target_Enemy : MonoBehaviour
{
    private int currentHealth;

    public void DecreaseHealthWithValue(int value) => currentHealth -= value;
    public void IncreaseHealthWithValue(int value) => currentHealth += value;
}
