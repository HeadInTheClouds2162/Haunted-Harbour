using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementComponent : MonoBehaviour, IInputReceiver
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;


    private GroundController _onGround;
    private Rigidbody2D _rb;
    private float _currentMoveDirection;

    //TEMPORARY VARIABLES?
    private void Awake()
    {
        _rb =  GetComponent<Rigidbody2D>();
        _onGround = GetComponent<GroundController>();
    }

    public void BindControls(PlayerInput reference)
    {
        reference.actions["Jump"].performed += TryJump;
        reference.actions["Move"].performed += SetMoveDirection;
    }

  

    public void UnbindControls(PlayerInput reference)
    {
        reference.actions["Jump"].performed -= TryJump;
        reference.actions["Move"].performed -= SetMoveDirection;
    }

    private void Jump()
    {
        _rb.linearVelocityY = 0;
        _rb.AddForceY(jumpForce, ForceMode2D.Impulse);
    }

    private void TryJump(InputAction.CallbackContext _)
    {
        if(CanJump() ) Jump();
    }

    private bool CanJump()
    {
        return _onGround.IsGrounded();

    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        _rb.AddForceX(_currentMoveDirection * moveSpeed);
    }
    
    private void SetMoveDirection(InputAction.CallbackContext obj)
    {
        _currentMoveDirection = obj.ReadValue<float>();
    }

    

}