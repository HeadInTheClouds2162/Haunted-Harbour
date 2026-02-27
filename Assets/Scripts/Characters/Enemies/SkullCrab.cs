using UnityEngine;

public class SkullCrab : Enemy
{

    [SerializeField] private float detectionDistance; 
    [SerializeField] LayerMask detectionLayers;
    private void TurnAround()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    
    private readonly RaycastHit2D[] _results = new RaycastHit2D[2];

    bool DetectInFront()
    {
        var size = Physics2D.RaycastNonAlloc(transform.position, new Vector2(transform.localScale.x, 0), _results, detectionDistance, detectionLayers);
        if (size > 1)
            return true;
        return false;
    }
    
    protected override void Move()
    {
        rigidbody2D.AddForceX(speed * transform.localScale.x, ForceMode2D.Impulse);
        float currentSpeed = Mathf.Abs(rigidbody2D.linearVelocityX);
        if(DetectInFront())TurnAround();
        if (currentSpeed > maxSpeed)
            rigidbody2D.linearVelocityX = (currentSpeed / rigidbody2D.linearVelocityX) * maxSpeed;
    }

    
}
