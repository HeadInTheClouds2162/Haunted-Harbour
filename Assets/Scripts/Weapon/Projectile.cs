using System;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{ 
    [SerializeField] protected float damage = 1;
    [SerializeField] protected float lifeTime = 5;
    [SerializeField] protected float initialSpeed = 50;

    [SerializeField] protected bool rotateWithVelocity  = true;
    
    protected Rigidbody2D _rb;

    
    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

    }
    public void Shoot()
    {
        Shoot(transform.right);
    }
 
    public virtual void Shoot(Vector2 direction)
    {
        _rb.AddForce(direction * initialSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, lifeTime);
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.rigidbody && other.rigidbody.TryGetComponent(out IDamagable damagable))
        {
            ContactPoint2D col = other.contacts[0];
            damagable.TakeDamage(damage, col.normal, col.point);
        }
        Destroy(gameObject);
    }

    protected virtual void FixedUpdate()
    {
        if(rotateWithVelocity) transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_rb.linearVelocityY, _rb.linearVelocityX) * Mathf.Rad2Deg);
    }
}
