using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class JellyFish : Enemy
{
    private Transform _player;
    [SerializeField] float minBurstTime = 1f;
    [SerializeField] float maxBurstTime = 7f;
    
    [SerializeField] private float rotationSpeed = 2f;
    private float _timer;

    private void Start()
    {
        _timer = Random.Range(minBurstTime, maxBurstTime);
    }

    protected override void Move()
    {
        if (_player == null) return;

        
        Vector2 dir = (_player.position - transform.position);
        float dt = Time.deltaTime;
        
        float targetAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        
        _timer -= dt;

        if (_timer <= 0f)
        {
            // Burst toward player
          
            rigidbody2D.AddForce(transform.right * speed, ForceMode2D.Impulse);

            // Clamp speed
            float currentSpeed = rigidbody2D.linearVelocity.magnitude;
            if (currentSpeed > maxSpeed)
            {
                rigidbody2D.linearVelocity = rigidbody2D.linearVelocity.normalized * maxSpeed;
            }

            // Reset timer
            _timer = Random.Range(minBurstTime, maxBurstTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Player target))
         _player = target.transform;
    }
}