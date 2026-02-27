using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileWeapon : Weapon
{

    [SerializeField] private Projectile prefab;
    [SerializeField] private Transform firePoint;
    [SerializeField, Min(1)] private int numProjectiles = 4; // We can use the min tag to make sure there's always atleast 1 projectile
    [SerializeField, Range(0,90)] private float inaccuracyInDegrees = 15; //<< We can use the Range tag to make this a slider
    
    protected override void Attack()
    {
        //OLD
        //Projectile projectile = Instantiate(prefab, firePoint.position, firePoint.rotation);
        //projectile.Shoot();
        
        //NEW
        for (int i = 0; i < numProjectiles; i++)
        {
            Vector3 rotation = firePoint.rotation.eulerAngles;
            rotation.z += Random.Range(-inaccuracyInDegrees, inaccuracyInDegrees);
            Projectile projectile = Instantiate(prefab, firePoint.position, Quaternion.Euler(rotation));
            projectile.Shoot();
        }
    }

    // Use AI to generate OnDrawGizmos, it's easy and usually a good result.
    // This code does not need to be optimized, and is always entirely localized making it perfect for AI.
    
    private void OnDrawGizmos()
    {
        if (firePoint == null) return;

        float length = 3f;

        // Fill the cone slice
#if UNITY_EDITOR
    UnityEditor.Handles.color = new Color(1f, 1f, 0f, 0.15f); // translucent yellow
    UnityEditor.Handles.DrawSolidArc(
        firePoint.position,
        Vector3.forward,                                      // normal axis (2D game = Z)
        Quaternion.Euler(0, 0, -inaccuracyInDegrees) * firePoint.right, // start direction
        inaccuracyInDegrees * 2f,                            // total angle
        length
    );
#endif

        // Boundary rays
        Gizmos.color = Color.yellow;
        Vector3 leftDir  = Quaternion.Euler(0, 0,  inaccuracyInDegrees) * firePoint.right;
        Vector3 rightDir = Quaternion.Euler(0, 0, -inaccuracyInDegrees) * firePoint.right;
        Gizmos.DrawRay(firePoint.position, leftDir  * length);
        Gizmos.DrawRay(firePoint.position, rightDir * length);

        // Center ray
        Gizmos.color = Color.red;
        Gizmos.DrawRay(firePoint.position, firePoint.right * length);

        // Arc
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
