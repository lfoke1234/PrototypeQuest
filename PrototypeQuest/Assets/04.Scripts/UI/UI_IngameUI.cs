using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_IngameUI : MonoBehaviour
{
    private PlayerStat playerStat;
    [SerializeField] private GameObject[] uis;

    [Header("Player")]
    [SerializeField] private Slider slider;

    [Header("Skills")]
    [SerializeField] private Image eImage;
    [SerializeField] private Image qImage;

    private SkillManager skills;

    private void Start()
    {
        playerStat = PlayerManager.instance.player.GetComponentInParent<PlayerStat>();

        if (playerStat != null)
        {
            playerStat.onHealthChanged += UpdateHealthUI;
        }

        skills = SkillManager.instance;

        UpdateHealthUI();
    }

    private void Update()
    {
        CheckCooldown();
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = playerStat.GetMaxHealth();
        slider.value = playerStat.currentHealth;
    }

    private void CheckCooldown()
    {
        UpdateESkillUI(eImage);
        UpdateQSkillUI(qImage);
    }

    private void UpdateESkillUI(Image _image)
    {
        _image.fillAmount = (skills.eSkill.coolDown - skills.eSkill.GetColldownTimer()) / skills.eSkill.coolDown;
    }

    private void UpdateQSkillUI(Image _image)
    {
        _image.fillAmount = (skills.qSkill.coolDown - skills.qSkill.GetColldownTimer()) / skills.qSkill.coolDown;
    }

    public void DisbleInGameUI()
    {
        foreach (GameObject item in uis)
        {
            item.SetActive(false);
        }
    }

    public void EnableInGameUI()
    {
        foreach (GameObject item in uis)
        {
            item.SetActive(true);
        }
    }
}
