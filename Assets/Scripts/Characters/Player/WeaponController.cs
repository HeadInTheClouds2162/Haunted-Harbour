using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour, IInputReceiver
{
    [SerializeField] private Weapon currentWeapon;

    public void BindControls(PlayerInput reference)
    {
        reference.actions["Shoot"].performed += Shoot;
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
    }
    
    
        
    
    
    
}
