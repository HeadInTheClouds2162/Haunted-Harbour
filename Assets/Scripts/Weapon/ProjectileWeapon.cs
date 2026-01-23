using UnityEngine;

public class ProjectileWeapon : Weapon
{

    [SerializeField] private Projectile prefab;
    [SerializeField] private Transform firePoint;
    
    
    protected override void Attack()
    {
        Instantiate(prefab, firePoint.position, firePoint.rotation);
    }
    
}
