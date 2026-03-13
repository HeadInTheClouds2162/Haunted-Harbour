using UnityEngine;

public class RoketExplode : MonoBehaviour
{
    public float explosionRadius = 100f;
    public float explosionDamage = 50f;
    public GameObject explosionEffect;
    public GameObject explosionEffect2;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }


    void Explode()
    {

        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Instantiate(explosionEffect2, transform.position, Quaternion.identity);
        }


        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hits)
        {


            var damageable = hit.GetComponent<IDamagable>();

            if (damageable != null)
            {
                Vector2 direction = (hit.transform.position - transform.position).normalized;
                Vector2 position = transform.position;

                damageable.TakeDamage(explosionDamage, direction, position);
            }
            
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (rb.position - (Vector2)transform.position).normalized;
                rb.AddForce(direction * 700f);
            }


        }


        Destroy(gameObject);
    }
    

}
