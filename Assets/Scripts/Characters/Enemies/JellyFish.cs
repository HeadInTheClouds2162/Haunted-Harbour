using UnityEngine;

public class JellyFish : MonoBehaviour, IDamagable
{
    [SerializeField] private float health;
    
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
