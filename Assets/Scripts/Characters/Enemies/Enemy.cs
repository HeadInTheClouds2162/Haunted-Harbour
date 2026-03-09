using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] protected float health;
    [SerializeField] protected float speed;
    [SerializeField] protected float maxSpeed;
    protected Animator _animator;
    protected Rigidbody2D rigidbody2D;
    [SerializeField] private ParticleSystem hurtParticles;
    
    protected void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
    
    public void TakeDamage(float damage, Vector2 direction, Vector2 position)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }

        if (hurtParticles)
        {
            hurtParticles.transform.SetPositionAndRotation(position, Quaternion.LookRotation(direction));
            hurtParticles.Play();
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
