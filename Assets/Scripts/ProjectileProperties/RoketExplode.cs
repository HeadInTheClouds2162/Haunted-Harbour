using UnityEngine;

public class RoketExplode : Projectile
{
    public float explosionRadius = 100f;
    public float explosionDamage = 50f;
    public GameObject explosionEffect;
    public GameObject explosionEffect2;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip explosionSound;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
        base.OnCollisionEnter2D(collision);
    }

    void Explode()
    {

        GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
        AudioSource audio = effect.GetComponent<AudioSource>();

        if (audio != null && explosionSound != null)
        {
            audio.PlayOneShot(explosionSound, 3f);
        }

        if (explosionEffect2 != null)
        {
            Instantiate(explosionEffect2, transform.position, Quaternion.identity);
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hits)
        {

            Rigidbody2D rb = hit.attachedRigidbody;
            if (!rb) continue;
            
            Vector2 direction = (hit.transform.position - transform.position).normalized;


            if (rb.TryGetComponent(out IDamagable damagable))
            {
                int hitLayerBit = 1 << hit.gameObject.layer;
                int includeMask = _rb.includeLayers.value;
                bool layerExcluded = (includeMask & hitLayerBit) != 0;

                if (layerExcluded)
                {
                    damagable.TakeDamage(explosionDamage, direction, transform.position, 0);
                }
            }

            rb.AddForce(direction * knockbackamount, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.3f, 0f, 0.35f);
        Gizmos.DrawSphere(transform.position, explosionRadius);
        Gizmos.color = new Color(1f, 0.3f, 0f, 0.9f);
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}