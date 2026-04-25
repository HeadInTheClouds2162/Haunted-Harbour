using System;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] protected float speed;
    [SerializeField] protected float maxSpeed;
    protected Animator _animator;
    protected Rigidbody2D rigidbody2D;
    [SerializeField] private ParticleSystem hurtParticles;
    public Action OnHealthChanged { get; set; }
    [field: SerializeField] public float MaxHealth { get; set; }

    public float CurrentHealth
    {
        get => _healt;
        set
        {
            _healt = Mathf.Clamp(value, 0, MaxHealth);
            if (_healt <= 0) Die();
            OnHealthChanged?.Invoke();
        }
    }
    private float _healt;
    
    protected void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        CurrentHealth = MaxHealth;
    }
    
    public void TakeDamage(float damage, Vector2 direction, Vector2 position)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
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
