using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class WeaponController : MonoBehaviour, IInputReceiver
{
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Transform hand;
    [SerializeField] private bool startRight = true;
    [SerializeField] private Weapon[] weapons;
    private int WeaponIndex;
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
        weapons = GetComponentsInChildren<Weapon>(true);
    }
    
    public void BindControls(PlayerInput reference)
    {
        reference.actions["Shoot"].performed += Shoot;
        reference.actions["MousePosition"].performed += PointHandTarget;
        reference.actions["Previous Weapon"].performed += PreviousWeapon;
        reference.actions["Next Weapon"].performed += NextWeapon;
        _camera = Camera.main;
    }

    private void PreviousWeapon(InputAction.CallbackContext obj)
    {
        WeaponIndex += 1;
        if (WeaponIndex >= weapons.Length)
            WeaponIndex = 0;
        SetWeapon(weapons[WeaponIndex]);
    }

    private void NextWeapon(InputAction.CallbackContext obj)
    {
        WeaponIndex -= 1;
        if (WeaponIndex < 0)
            WeaponIndex = weapons.Length - 1;
        SetWeapon(weapons[WeaponIndex]);
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
        reference.actions["Previous Weapon"].performed -= PreviousWeapon;
        reference.actions["Next Weapon"].performed -= NextWeapon;
    }

    private void PointHandTarget(InputAction.CallbackContext obj)
    {
        Vector2 mousePosition = obj.ReadValue<Vector2>();
        mousePosition = _camera.ScreenToWorldPoint(mousePosition);
        PointHandTarget(mousePosition);
        player.SetlookDirection( mousePosition.x <= transform.position.x);
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
        
        if(!startRight) run *= -1;
        if(!startRight) rise *= -1;
        
        float angle = Mathf.Atan2(rise, run) * Mathf.Rad2Deg;
        hand.rotation = Quaternion.Euler(0, 0, angle);

        int faceRight = 1;
        
        if(angle is > 90 or < -90) faceRight = -1;
        
        hand.transform.localScale = new Vector3(1, faceRight, 1);
    }
}