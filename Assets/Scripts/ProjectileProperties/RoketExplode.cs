using UnityEngine;

public class RoketExplode : MonoBehaviour
{
    [SerializeField] private float knockbackamount = 5f;
    public float explosionRadius = 100f;
    public float explosionDamage = 50f;
    public GameObject explosionEffect;
    public GameObject explosionEffect2;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip explosionSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }

    void Explode()
    {
        // Spawn explosion effect
        GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // Play louder explosion sound (2f = twice as loud)
        AudioSource audio = effect.GetComponent<AudioSource>();
        if (audio != null && explosionSound != null)
        {
            audio.PlayOneShot(explosionSound, 3f);
        }

        // Optional second effect
        if (explosionEffect2 != null)
        {
            Instantiate(explosionEffect2, transform.position, Quaternion.identity);
        }

        // Damage + force
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hits)
        {
            var damageable = hit.GetComponent<IDamagable>();

            if (damageable != null)
            {
                Vector2 direction = (hit.transform.position - transform.position).normalized;
                Vector2 position = transform.position;

                damageable.TakeDamage(explosionDamage, direction, position,knockbackamount );
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