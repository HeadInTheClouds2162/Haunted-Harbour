using UnityEngine;

public class JellyFish : Enemy
{

    protected override void Move()
    {
        rigidbody2D.AddForceX(speed * transform.localScale.x, ForceMode2D.Impulse);
        float currentSpeed = Mathf.Abs(rigidbody2D.linearVelocityX);
        if (currentSpeed > maxSpeed)
            rigidbody2D.linearVelocityX = (currentSpeed / rigidbody2D.linearVelocityX) * maxSpeed;
    public float timer =3f; 

    void Update()
    {
        timer -= Time.deltaTime;
            
    }
}
    
    

