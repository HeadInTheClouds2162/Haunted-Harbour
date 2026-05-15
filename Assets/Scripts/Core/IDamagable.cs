using System;
using UnityEngine;

public interface IDamagable
{
    public void TakeDamage(float damage, Vector2 direction, Vector2 position, float knockback);
    public Action OnHealthChanged { get; set; }
    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    
}




