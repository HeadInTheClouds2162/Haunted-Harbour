using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour, IInputReceiver
{
    [SerializeField] private Weapon currentWeapon;
<<<<<<< HEAD
    [SerializeField] private Transform Hand;
=======
    [SerializeField] private Transform hand;
>>>>>>> ee89f2fa45dad581eca9e1fb827298ca0ccaf19c
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

<<<<<<< HEAD
    private void PointHandTarget(InputAction.CallbackContext obj)
    {
        Vector2 mousePosition = obj.ReadValue<Vector2>();
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        float rise =  mousePosition.y - Hand.position.y;
        float run = mousePosition.x - Hand.position.x;
        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        Hand.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    
    
    
    
    
        
    
    
    
=======
    private void Test(InputAction.CallbackContext obj)
    {
        Vector2 mousePos =  obj.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        float rise = mousePos.y - hand.position.y;
        float run =  mousePos.x - hand.position.x;
        float angle = Mathf.Atan(rise / run) *Mathf.Rad2Deg;
        hand.transform.rotation = Quaternion.Euler(0, 0, angle);
    }






>>>>>>> ee89f2fa45dad581eca9e1fb827298ca0ccaf19c
}
