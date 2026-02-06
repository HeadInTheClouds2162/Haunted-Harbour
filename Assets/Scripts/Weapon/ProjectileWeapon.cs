using UnityEngine;

public class ProjectileWeapon : Weapon
{

    [SerializeField] private Projectile prefab;
    [SerializeField] private Transform firePoint;
    
    
    protected override void Attack()
    {
        Projectile projectile = Instantiate(prefab, firePoint.position, firePoint.rotation);
        
        //TODO: Apply a direction manually.
        projectile.Shoot();
    }
    
}
