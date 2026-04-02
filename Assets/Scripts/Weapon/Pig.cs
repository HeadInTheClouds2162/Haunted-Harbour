using UnityEngine;

public class PigCannon : MonoBehaviour
{
    public GameObject cannonballPrefab;
    public Transform firePoint;
    public float fireForce = 12f;

    private float fireTimer = 3f; // counts down from 3 seconds

    private void Awake()
    {
        Fire();
    }

    private void FixedUpdate()
    {
        // countdown
        fireTimer -= Time.fixedDeltaTime;

        // if timer is done → fire
        if (fireTimer <= 0f)
        {
            fireTimer = 3f; // reset timer
        }
    }

    private void Fire()
    {
        GameObject ball = Instantiate(cannonballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        // fire in the direction the cannon is facing
        rb.AddForce(firePoint.right * fireForce, ForceMode2D.Impulse);
    }
}