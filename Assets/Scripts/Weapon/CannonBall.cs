using UnityEngine;

public class CannonBall : Projectile

{
    protected override void OnCollisionEnter2D(Collision2D other)
    {
        Rigidbody2D rb = other.rigidbody;
        if (!rb) return;
        ContactPoint2D col = other.contacts[0];
        if (rb.TryGetComponent(out JellyFish jellyFish))
        {
            _rb.linearVelocity = Vector2.Reflect(_rb.linearVelocity, -col.normal);
            jellyFish.TakeDamage(damage * _rb.linearVelocity.magnitude * 0.1f, col.normal, col.point, knockbackamount );

        }
        else if (rb.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(damage * _rb.linearVelocity.magnitude, col.normal, col.point, knockbackamount );
        }
        else
        {
            enabled = false; // Disable ourselves, if we've hit nothing (Or the ground)
        }
    }
}
