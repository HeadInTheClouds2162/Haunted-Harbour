using System;
using UnityEngine;

public class HealthbarScript : MonoBehaviour
{
    private IDamagable target;

    private void Awake()
    {
        target = GetComponentInParent<IDamagable>();
    }

    void UpdateHealthPercent()
    {
        
    }
}
