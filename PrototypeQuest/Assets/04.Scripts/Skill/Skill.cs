using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float coolDown;
    public float coolDownTimer;
    public float amount;
    protected Player player;

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }

    protected virtual void Update()
    {
        coolDownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if (coolDownTimer < 0 && player.stat.currentMana > amount)
        {
            UseSkill();
            coolDownTimer = coolDown;
            return true;
        }

        return false;
    }

    public virtual void UseSkill()
    {

    }

    public float GetColldownTimer()
    {
        return coolDownTimer;
    }
}
