using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class JellyFish : Enemy
{
    private static readonly int Hurt = Animator.StringToHash("Hurt");
    [SerializeField] float minBurstTime = 1f;
    [SerializeField] float maxBurstTime = 7f;
    [SerializeField] private float damage = 3;
    [SerializeField] private float knockbackamount = 5f;
    [SerializeField] private float torqueStrength = 5f;
    private float _timer;

    protected override void Start()
    {
        base.Start();
        _timer = Random.Range(minBurstTime, maxBurstTime);
    }

    protected override void Move()
    {
        if (_target == null) return;

        float dt = Time.deltaTime;

        // Get angle to target
        Vector2 dir = (_target.position - transform.position);
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        float currentAngle = transform.eulerAngles.z;
        float angleDelta = Mathf.DeltaAngle(currentAngle, targetAngle);

        float torque = Mathf.Sign(angleDelta) * torqueStrength;//Sign determines positive or negative
        

        if (Mathf.Abs(angleDelta) < 15f)
        {
            torque *= Mathf.Abs(angleDelta) / 15f;
        }

        rigidbody2D.AddTorque(torque);

        _timer -= dt;

        if (_timer <= 0f)
        {
            rigidbody2D.AddForce(transform.right * speed, ForceMode2D.Impulse);

            float currentSpeed = rigidbody2D.linearVelocity.magnitude;
            if (currentSpeed > maxSpeed)
            {
                rigidbody2D.linearVelocity = rigidbody2D.linearVelocity.normalized * maxSpeed;
            }

            _timer = Random.Range(minBurstTime, maxBurstTime);
        }
    }

    public override void TakeDamage(float damage, Vector2 hitPoint, Vector2 hitNormal, float knockback)
    {
        _animator.SetTrigger(Hurt);
        base.TakeDamage(damage, hitNormal, hitPoint, knockback);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.rigidbody && other.rigidbody.TryGetComponent(out Player target))
        {
            target.TakeDamage(damage,rigidbody2D.linearVelocity, transform.position, knockbackamount );
        }
    }
}   