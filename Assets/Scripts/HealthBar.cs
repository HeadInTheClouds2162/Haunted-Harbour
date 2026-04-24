using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private IDamagable target;
    public Image healthBar;

    public void Start()
    {
        
        target.OnHealthChanged += UpdateHealthPercent;
        UpdateHealthPercent();
    }

    private void Awake()
    {
        target = GetComponentInParent<IDamagable>();
    }

    private void UpdateHealthPercent()
    {
        healthBar.fillAmount = target.CurrentHealth / target.MaxHealth;
    }

    private void OnDestroy()
    {
        target.OnHealthChanged -= UpdateHealthPercent;
    }
}







