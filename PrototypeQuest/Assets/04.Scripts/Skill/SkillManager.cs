using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public SKill_ESkill eSkill { get; private set; }
    public Skill_QSkill qSkill { get; private set; }


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

        eSkill = GetComponent<SKill_ESkill>();
        qSkill = GetComponent<Skill_QSkill>();
    }
}
