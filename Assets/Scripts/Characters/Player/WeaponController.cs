using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour, IInputReceiver
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Transform hand;
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

    private void Test(InputAction.CallbackContext obj)
    {
        Vector2 mousePos =  obj.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        float rise = mousePos.y - hand.position.y;
        float run =  mousePos.x - hand.position.x;
        float angle = Mathf.Atan(rise / run) *Mathf.Rad2Deg;
        hand.transform.rotation = Quaternion.Euler(0, 0, angle);
    }






}
