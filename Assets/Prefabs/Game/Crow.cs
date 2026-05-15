using UnityEngine;

public class Crow : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    [SerializeField] Transform target;
    [SerializeField] float speed;
    [SerializeField] float bobHeight;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() 
    { 
        Vector3 direction = (target.position - transform.position).normalized;
        Vector3 vel = direction * (speed * Time.deltaTime);
        rigidbody2D.linearVelocity = vel;
        transform.position += new Vector3(0, Mathf.Sin(Time.time) * bobHeight, 0);
        // JENNY IF YOU WANT THE CROWS TO LOOK AT THE TARGETS, Look at Enemy.Move()
    }

}
