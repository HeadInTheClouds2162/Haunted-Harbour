using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour, IInputReceiver
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Transform hand;
    
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main; //<< this is called caching, Camera.main can be an expensive call.
    }

    public void BindControls(PlayerInput reference)
    {
        reference.actions["Shoot"].performed += Shoot;
        reference.actions["MousePosition"].performed += PointHandTarget;
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        bool isShooting = obj.ReadValueAsButton();
        if (!currentWeapon) return;
        
        if (isShooting)
            currentWeapon.StartAttacking();
        else
            currentWeapon.StopAttacking();
            
    }

    public void UnbindControls(PlayerInput reference)
    {
        reference.actions["Shoot"].performed -= Shoot;
        reference.actions["MousePosition"].performed -= PointHandTarget;
    }

    private void PointHandTarget(InputAction.CallbackContext obj)
    {
        Vector2 mousePosition = obj.ReadValue<Vector2>();
        mousePosition = _camera.ScreenToWorldPoint(mousePosition);
        float rise =  mousePosition.y - hand.position.y;
        float run = mousePosition.x - hand.position.x;
        float angle = Mathf.Atan2(rise, run) * Mathf.Rad2Deg;
        hand.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    
    
    
    
    
        
    
    
    
}
