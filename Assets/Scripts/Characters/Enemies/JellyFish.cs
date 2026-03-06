using UnityEngine;

public class JellyFish : Enemy
{
    [SerializeField] Transform player;
    [SerializeField] float burstInterval = 3f;
    private float timer;

    private void Start()
    {
        timer = burstInterval;
    }

    protected override void Move()
    {
        if (player == null) return;

        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            // Burst toward player
            Vector2 dir = (player.position - transform.position).normalized;
            rigidbody2D.AddForce(dir * speed, ForceMode2D.Impulse);

            // Clamp speed
            float currentSpeed = rigidbody2D.linearVelocity.magnitude;
            if (currentSpeed > maxSpeed)
            {
                rigidbody2D.linearVelocity = rigidbody2D.linearVelocity.normalized * maxSpeed;
            }

            // Reset timer
            timer = burstInterval;
        }
    }
}