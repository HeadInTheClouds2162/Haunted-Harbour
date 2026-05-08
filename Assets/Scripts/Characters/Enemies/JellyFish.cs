using UnityEngine;
using Random = UnityEngine.Random;

public class JellyFish : Enemy
{
    [SerializeField] float minBurstTime = 1f;
    [SerializeField] float maxBurstTime = 7f;

    [SerializeField] private float torqueStrength = 5f;
    private float _timer;

    protected override void Start()
    {
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
}