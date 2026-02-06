using System;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{ 
    [SerializeField] private float damage = 1;
    [SerializeField] private float lifeTime = 5;
    [SerializeField] private float initialSpeed = 50;

    [SerializeField] private bool rotateWithVelocity  = true;
    
    private Rigidbody2D _rb;

    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

    }
    public void Shoot()
    {
        _rb.AddForce(transform.right * initialSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, lifeTime);
    }
 
    public void Shoot(Vector2 direction)
    {
        _rb.AddForce(direction * initialSpeed, ForceMode2D.Impulse);
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.rigidbody && other.rigidbody.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(rotateWithVelocity) transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_rb.linearVelocityY, _rb.linearVelocityX) * Mathf.Rad2Deg);
    }
}
