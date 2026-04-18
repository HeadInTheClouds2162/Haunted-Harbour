using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour, IInputReceiver
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Transform hand;
    private Player player;
    
    public void SetWeapon(Weapon weapon)
    {
        currentWeapon.StopAttacking();
        currentWeapon.gameObject.SetActive(false);
        currentWeapon = weapon;
        weapon.gameObject.SetActive(true);
    }
    private Camera _camera;

    public void Awake()
    {
        player = GetComponent<Player>();
    }
    

    public void BindControls(PlayerInput reference)
    {
        reference.actions["Shoot"].performed += Shoot;
        reference.actions["MousePosition"].performed += PointHandTarget;
        _camera = Camera.main;
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        bool isShooting = obj.ReadValueAsButton();
        Shoot(isShooting);
        
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
        PointHandTarget(mousePosition);
        player.SetlookDirection( (mousePosition.x <= transform.position.x));
    }

    public void Shoot(bool state)
    {
        if (!currentWeapon) return;
        if (state)
            currentWeapon.StartAttacking();
        else
            currentWeapon.StopAttacking();
    }

    public void PointHandTarget(Vector2 state)
    {
        float rise =  state.y - hand.position.y;
        float run = state.x - hand.position.x;
        float angle = Mathf.Atan2(rise, run) * Mathf.Rad2Deg;
        hand.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    
    
    }


