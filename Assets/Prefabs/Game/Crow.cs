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
    { rigidbody2D.MovePosition(new Vector2(rigidbody2D.position.x, rigidbody2D.position.y + Mathf.Sin(Time.time) * bobHeight));
       rigidbody2D.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

}
