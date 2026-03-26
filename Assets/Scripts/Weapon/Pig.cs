using UnityEngine;

public class Pig : Weapon
{
    public GameObject cannonballPrefab;
    public Transform firePoint;
    public float fireForce = 10f;
    public float fireInterval = 3f;

    private float _timer;

    private void Start()
    {
        FireCannonball();
        _timer = fireInterval;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            _timer = fireInterval;
        }
    }

    private void FireCannonball()
    {
        GameObject ball = Instantiate(cannonballPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();

        rb.AddForce(firePoint.right * fireForce, ForceMode2D.Impulse);
    }

    protected override void Attack()
    {
        
    }
}

