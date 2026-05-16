using System;
using UnityEngine;

[SelectionBase]
public class Enemy : MonoBehaviour, IDamagable
{
    private static readonly int MovementX = Animator.StringToHash("MovementX");
    private static readonly int IsAlive = Animator.StringToHash("IsAlive");
    [SerializeField] protected float speed;
    [SerializeField] protected float maxSpeed;
    protected Animator _animator;
    protected Rigidbody2D rigidbody2D;
    [SerializeField] private ParticleSystem hurtParticles;
    public Action OnHealthChanged { get; set; }
    [field: SerializeField] public float MaxHealth { get; set; }
    [SerializeField] private bool lookDirectionStartRight = true;
    [SerializeField] private Transform flipTransform;

    [SerializeField] private bool cheatAlwaysSeekPlayer;
    
    private bool _isFacingright; 

    
    protected Transform _target;

    
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
    
    protected virtual void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        CurrentHealth = MaxHealth;

     
    }

    protected virtual void Start()
    {
        if (cheatAlwaysSeekPlayer)
        {
            _target = Player.playerTransform;
            OnNewTarget();
        }
    }

    public virtual void TakeDamage(float damage, Vector2 direction, Vector2 position, float knockback)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            
            Die();
        }
        rigidbody2D.AddForce(direction  * knockback,  ForceMode2D.Impulse);
        if (hurtParticles)
        {
            hurtParticles.transform.SetPositionAndRotation(position, Quaternion.Euler(0,0,Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));
            hurtParticles.Play();
        }
    }



    protected virtual void Die()
    {
        if (hurtParticles)
        {
            hurtParticles.transform.SetParent(null);
            Destroy(hurtParticles.gameObject, hurtParticles.main.duration); // Destroy the particles after they're done playing
        }

        _animator?.SetBool(IsAlive, false);
        rigidbody2D.simulated = false;
        Destroy(gameObject, 3);
    }

    protected virtual void Move()
    {
        bool lookDirection = _target ? transform.position.x < _target.position.x : rigidbody2D.linearVelocityX > 0;
        SetlookDirection(lookDirection);
       
    }
    
    private void FixedUpdate()
    {
        _animator?.SetFloat(MovementX, Mathf.Abs(rigidbody2D.linearVelocityX));
        if (cheatAlwaysSeekPlayer) _target = Player.playerTransform;
        Move();

    }

    public void SetlookDirection(bool faceRight)
    {
        if (_isFacingright == faceRight) return;
        _isFacingright = faceRight;

        Vector3 vec = flipTransform.localScale;
        flipTransform.localScale = new Vector3((faceRight ^ lookDirectionStartRight) ? -Mathf.Abs(vec.x) : Mathf.Abs(vec.x), vec.y, vec.z);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (cheatAlwaysSeekPlayer) return;
        if(other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Player p))
        {
            _target = p.transform;
            OnNewTarget();
        }
    }

    protected virtual void OnNewTarget(){}
}
