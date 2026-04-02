using UnityEngine;

public class Cannonball : MonoBehaviour
{
    public GameObject explosionPrefab;

    private bool isGrounded = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
      
        if (collision.collider.name == "Ground")
        {
            isGrounded = true;
        }

        // If grounded, explode
        if (isGrounded == true)
        {
            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}