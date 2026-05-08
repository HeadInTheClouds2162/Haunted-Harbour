using System;
using UnityEngine;

public class SeaHag : Enemy
{
    [SerializeField] float MaxTotalSecondsTouchedForDamage = 1f;
    private float _timer;
    [SerializeField] private float damage;
    [SerializeField] private float bobHeight = 0.4f;
    [SerializeField] private float bobWidth = 0.4f;
    protected override void Move()
    {
base.Move();
        if (_target == null)
        {
            rigidbody2D.MovePosition(new Vector2(Mathf.Cos(Time.time) * bobWidth, Mathf.Sin(Time.time) * bobHeight));
            return;
        }
        

        Vector2 toSeahag = (_target.position-transform.position).normalized;
        /*
        Vector2 playerForward = new Vector2(_target.localScale.x, 0).normalized;
        float dot = Vector2.Dot(playerForward, toSeahag); //if from the seahag the player is looking at  us

        if (dot < 0f)
        {
            //rigidbody2D.linearVelocity = Vector2.zero;
            return;
        }
        */
        
        rigidbody2D.AddForce(toSeahag * speed, ForceMode2D.Impulse);
        
        if (rigidbody2D.linearVelocity.magnitude > maxSpeed)
        {
            rigidbody2D.linearVelocity = rigidbody2D.linearVelocity.normalized * maxSpeed;
        }
        float dt = Time.deltaTime;
        _timer += dt;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.rigidbody && other.rigidbody.TryGetComponent(out Player target))
        {
            
            if (_timer >= 1f)
            {
                target.TakeDamage(damage,Vector2.zero, Vector2.zero);
                _timer = 0f;
            }
        }
            
    }
}
