using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private Projectile prefab;
    [SerializeField] private Transform firePoint;
    [SerializeField, Min(1)] private int numProjectiles = 4;
    [SerializeField] private Vector2 recoil = new Vector2(-0.1f, 0.01f);
    [SerializeField, Range(0,90)] private float inaccuracyInDegrees = 15;

    [SerializeField] private AudioSource audioSource;   // <— added
    [SerializeField] private AudioResource shootSound;      // <— added

    private CinemachineImpulseSource _recoil;

    private void Awake()
    {
        _recoil = GetComponent<CinemachineImpulseSource>();
    }

    protected override void Attack()
    {
        // Play sound every time the weapon fires
        if(shootSound)
        {
            audioSource.resource = (shootSound);
            audioSource.Play();
        }

        Vector3 rot = firePoint.rotation.eulerAngles;

        for (int i = 0; i < numProjectiles; i++)
        {
            Vector3 rotation = rot;
            rotation.z += Random.Range(-inaccuracyInDegrees, inaccuracyInDegrees);
            Projectile projectile = Instantiate(prefab, firePoint.position, Quaternion.Euler(rotation));
            projectile.Shoot(transform.root.gameObject.layer);
        }

        float rads = Mathf.Deg2Rad * rot.z;
        _recoil?.GenerateImpulseWithVelocity(new Vector3(Mathf.Cos(rads) * recoil.x, Mathf.Sin(rads)*recoil.y, 0));
    }

    private void OnDrawGizmos()
    {
        if (firePoint == null) return;

        float length = 3f;

#if UNITY_EDITOR
        UnityEditor.Handles.color = new Color(1f, 1f, 0f, 0.15f);
        UnityEditor.Handles.DrawSolidArc(
            firePoint.position,
            Vector3.forward,
            Quaternion.Euler(0, 0, -inaccuracyInDegrees) * firePoint.right,
            inaccuracyInDegrees * 2f,
            length
        );
#endif

        Gizmos.color = Color.yellow;
        Vector3 leftDir  = Quaternion.Euler(0, 0,  inaccuracyInDegrees) * firePoint.right;
        Vector3 rightDir = Quaternion.Euler(0, 0, -inaccuracyInDegrees) * firePoint.right;
        Gizmos.DrawRay(firePoint.position, leftDir  * length);
        Gizmos.DrawRay(firePoint.position, rightDir * length);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(firePoint.position, firePoint.right * length);

        Gizmos.color = Color.yellow;
        int segments = 20;
        for (int i = 0; i < segments; i++)
        {
            float angleA = Mathf.Lerp(-inaccuracyInDegrees, inaccuracyInDegrees, (float)i / segments);
            float angleB = Mathf.Lerp(-inaccuracyInDegrees, inaccuracyInDegrees, (float)(i + 1) / segments);
            Vector3 dirA = Quaternion.Euler(0, 0, angleA) * firePoint.right;
            Vector3 dirB = Quaternion.Euler(0, 0, angleB) * firePoint.right;
            Gizmos.DrawLine(
                firePoint.position + dirA * length,
                firePoint.position + dirB * length
            );
        }
    }
}