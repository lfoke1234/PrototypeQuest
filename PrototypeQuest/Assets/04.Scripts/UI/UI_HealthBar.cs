using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Enemy ememy;
    private EnemyStat myStats;
    private Slider slider;

    [SerializeField] protected GameObject sliderObj;
    [SerializeField] protected bool reverseSlider;

    [SerializeField] protected Vector3 sliderOffset = new Vector3(0, 2f, 0);

    private void Start()
    {
        ememy = GetComponentInParent<Enemy>();
        slider = GetComponentInChildren<Slider>();
        myStats = GetComponentInParent<EnemyStat>();

        myStats.onHealthChanged += UpdateHealthUI;

        UpdateHealthUI();
    }

    private void Update()
    {
        RotateSlider();
    }

    private void RotateSlider()
    {
        sliderObj.transform.position = transform.position + sliderOffset;

        if (reverseSlider)
        {
            sliderObj.transform.LookAt(2 * sliderObj.transform.position - Camera.main.transform.position);

            Vector3 currentRotation = sliderObj.transform.eulerAngles;
            sliderObj.transform.eulerAngles = new Vector3(-currentRotation.x, currentRotation.y, currentRotation.z);
        }
        else
        {
            sliderObj.transform.LookAt(2 * sliderObj.transform.position - Camera.main.transform.position);
        }

        Vector3 adjustedRotation = sliderObj.transform.eulerAngles;
        sliderObj.transform.eulerAngles = new Vector3(adjustedRotation.x, 0, 0);
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = myStats.GetMaxHealth();
        slider.value = myStats.currentHealth;
    }

    public void DisableHPBar()
    {
        slider.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        myStats.onHealthChanged -= UpdateHealthUI;
    }
}
