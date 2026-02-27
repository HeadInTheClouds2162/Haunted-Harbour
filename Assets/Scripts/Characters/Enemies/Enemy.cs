using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] protected float health;
    [SerializeField] protected float speed;
    [SerializeField] protected float maxSpeed;
    protected Animator animator;
    protected Rigidbody2D rigidbody2D;
    
    protected void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    
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

    protected virtual void Move()
    {
        
    }
    
    private void FixedUpdate()
    {
        Move();
    }
    
}
