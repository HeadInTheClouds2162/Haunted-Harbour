using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] private Weapon WeaponPrefab;
    private void Awake()
    {
        WeaponPrefab = Instantiate(WeaponPrefab, transform);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody &&  other.attachedRigidbody.TryGetComponent(out WeaponController controller))
         OnPickup(controller);
    }

    private void OnPickup(WeaponController controller)
    {
        controller.AddWeapon(WeaponPrefab);
        Destroy(gameObject);
    }
}