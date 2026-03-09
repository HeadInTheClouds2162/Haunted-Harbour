using UnityEngine;

public class SeaHag : Enemy
{
    private Transform _player;


    protected override void Move()
    {
        if (_player == null) return;
        Vector2 toSeahag = (_player.position-transform.position).normalized;
        Vector2 playerForward = new Vector2(_player.localScale.x, 0).normalized;
        float dot = Vector2.Dot(playerForward, toSeahag); //if from the seahag the player is looking at  us

        if (dot < 0f)
        {
            //rigidbody2D.linearVelocity = Vector2.zero;
            return;
        }
        
        rigidbody2D.AddForce(toSeahag * speed, ForceMode2D.Impulse);
        
        if (rigidbody2D.linearVelocity.magnitude > maxSpeed)
        {
            rigidbody2D.linearVelocity = rigidbody2D.linearVelocity.normalized * maxSpeed;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Player target))
            _player = target.transform;
    }
}
