using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    private Player player;
    private PlayerInputController playerInput;

    public bool isAttacking { get; private set; }

    private int attackCount;
    private float attackTimer;
    private float lastAttackTime;

    private float radious;

    [SerializeField] private Button eSkillButton;
    [SerializeField] private Button qSkillButton;
    [SerializeField] private Button attackButton;

    private void Start()
    {
        player = GetComponent<Player>();
        AssigneKey();

        eSkillButton.onClick.AddListener(() => UseESkillMobile());
        qSkillButton.onClick.AddListener(() => UseQSkillMobile());
        attackButton.onClick.AddListener(() => Attack());
    }

    private void Update()
    {
        attackTimer -= Time.deltaTime;
        CheckSkillInput();
    }

    private void Attack()
    {
        if (isAttacking || DialogueManager.instance.isDialgoueActive || GameManager.Instance.isPlayCutScene || player.stat.isDead) return;

        if (lastAttackTime + 2f < Time.time || attackCount > 1)
        {
            attackCount = 0;
        }

        lastAttackTime = Time.time;

        Transform closestEnemy = FindClosestEnemy(5f);
        if (closestEnemy != null)
        {
            RotateImmediatelyTowards(closestEnemy.position);
        }

        player.animator.SetInteger("AttackCount", attackCount);
        player.animator.SetTrigger("Attack");

        SetBusyAttack(true);

        attackCount++;
    }

    private void CheckSkillInput()
    {
        if (GameManager.Instance.isPlayCutScene || player.stat.isDead)
            return;

        if(ProgressManager.instance.unlockESkill)
            UseESkill();

        if (ProgressManager.instance.unlockQSkill)
            UseQSkill();
    }

    public void UseESkill()
    {
        if (isAttacking || DialogueManager.instance.isDialgoueActive) return;


        if (Input.GetKeyDown(KeyCode.E) && SkillManager.instance.eSkill.CanUseSkill())
        {
            SetBusyAttack(true);
            Transform closestEnemy = FindClosestEnemy(5f);

            if (closestEnemy != null && isAttacking)
            {
                RotateImmediatelyTowards(closestEnemy.position);
            }

            SkillManager.instance.eSkill.UseSkill();
        }
    }

    public void UseESkillMobile()
    {
        if (isAttacking || DialogueManager.instance.isDialgoueActive) return;


        if (SkillManager.instance.eSkill.CanUseSkill())
        {
            SetBusyAttack(true);
            Transform closestEnemy = FindClosestEnemy(5f);

            if (closestEnemy != null)
            {
                RotateImmediatelyTowards(closestEnemy.position);
            }

            SkillManager.instance.eSkill.UseSkill();
        }
    }

    public void UseQSkill()
    {
        if (isAttacking || DialogueManager.instance.isDialgoueActive) return;

        Transform closestEnemy = FindClosestEnemy(5f);

        if (closestEnemy != null && isAttacking)
        {
            RotateImmediatelyTowards(closestEnemy.position);
        }

        if (Input.GetKeyDown(KeyCode.Q) && SkillManager.instance.qSkill.CanUseSkill())
        {
            SetBusyAttack(true);
            SkillManager.instance.qSkill.UseSkill();
        }
    }

    public void UseQSkillMobile()
    {
        if (isAttacking || DialogueManager.instance.isDialgoueActive) return;

        Transform closestEnemy = FindClosestEnemy(5f);

        if (closestEnemy != null)
        {
            RotateImmediatelyTowards(closestEnemy.position);
        }

        if (SkillManager.instance.qSkill.CanUseSkill())
        {
            SetBusyAttack(true);
            SkillManager.instance.qSkill.UseSkill();
        }
    }

    #region Attack rogic
    private Transform FindClosestEnemy(float range)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            Enemy enemy = collider.GetComponentInParent<Enemy>();
            if (enemy != null && enemy.stat.isDead == false)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = collider.transform;
                }
            }
        }

        return closestEnemy;
    }

    private void RotateImmediatelyTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0;

        Quaternion lookRotation = Quaternion.LookRotation(direction);

        transform.rotation = lookRotation;
    }
    #endregion

    public void SetBusyAttack(bool busy)
    {
        isAttacking = busy;
        player.animator.SetBool("isAttacking", isAttacking);
    }

    private void AssigneKey()
    {
        playerInput = player.playerInput;

        playerInput.Player.Attack.performed += ctx => Attack();
    }
}
