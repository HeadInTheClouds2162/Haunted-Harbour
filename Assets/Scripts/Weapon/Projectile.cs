using System;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{ 
    [SerializeField] protected float damage = 1;
    [SerializeField] protected float lifeTime = 5;
    [SerializeField] protected float initialSpeed = 50;
    [SerializeField] protected float knockbackamount = 5f;
    [SerializeField] protected bool rotateWithVelocity  = true;
    
    protected Rigidbody2D _rb;
    
    private TrailRenderer _trailRenderer;
    
    protected virtual void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _trailRenderer = GetComponentInChildren<TrailRenderer>();

    }
    public void Shoot(int layer)
    {
        Shoot(transform.right,layer );
    }
 
    public virtual void Shoot(Vector2 direction, int layer)
    {
        _rb.AddForce(direction * initialSpeed, ForceMode2D.Impulse);
        int val = 1 << layer;
        int val2 = 1 << (layer + 1);
        //I've made it so that the next layer down includes the projectile layer, THIS IS NOT GOOD DESIGN
        // We've run into a strange issue regarding how much time we have, and the needs of the project that this is what we get.
        _rb.excludeLayers = (val) | (val2); 
        _rb.includeLayers = ~((val) | val2);
        gameObject.layer = layer + 1;
        Destroy(gameObject, lifeTime);
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.rigidbody && other.rigidbody.TryGetComponent(out IDamagable damagable))
        {
            ContactPoint2D col = other.contacts[0];
            damagable.TakeDamage(damage, col.normal, col.point,  knockbackamount);
        }

        if (_trailRenderer)
        {
            _trailRenderer.transform.SetParent(null);
            _trailRenderer.autodestruct = true;
        }

        Destroy(gameObject);
    }

    protected virtual void FixedUpdate()
    {
        if(rotateWithVelocity) transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_rb.linearVelocityY, _rb.linearVelocityX) * Mathf.Rad2Deg);
    }
}
