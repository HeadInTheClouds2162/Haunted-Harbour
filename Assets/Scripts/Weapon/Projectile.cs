using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{ 
    [SerializeField]
    private Rigidbody2D rb;
    private float speed=5;

    private void Awake()
    {
        Shoot();
    }

    void Shoot()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.right * speed, ForceMode2D.Impulse);
        
    } 
      
      
   


}
