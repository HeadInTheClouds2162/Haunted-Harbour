using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementComponent : MonoBehaviour, IInputReceiver
{
    
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] public int MaxJump;
    private int NumJump;
    
    private GroundController _onGround;
    private Rigidbody2D _rb;
    private float _currentMoveDirection;
    private Animator _animator;

    [SerializeField] private float groundFriction = 2;
    [SerializeField] private float airFriction = 0;

    [SerializeField] private bool useAnimation = false;
    
    private static readonly int MovementX = Animator.StringToHash("MovementX");
    private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
    private static readonly int IsAlive = Animator.StringToHash("IsAlive");
    private static readonly int JumpID = Animator.StringToHash("Jump");

    
    private void OnEnable()
    {
        _onGround ??= GetComponent<GroundController>();
        _onGround.OnGroundStateChanged += UpdateFriction;
        UpdateFriction(false, _onGround.IsGrounded());
    }

    private void OnDisable()
    {
        _onGround.OnGroundStateChanged -= UpdateFriction;
    }
    
    private void UpdateFriction(bool oldState, bool newState)
    {
        _rb.linearDamping = newState ? groundFriction : airFriction;
       
    }

   
    

    //TEMPORARY VARIABLES?
    private void Awake()
    {
        _rb =  GetComponent<Rigidbody2D>();
        _onGround = GetComponent<GroundController>();
        _animator = GetComponentInChildren<Animator>();
        if (!useAnimation) return;
        _animator.SetBool(IsAlive, true);
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
        
        if (!useAnimation) return;
        _animator.SetBool(IsAlive , false);

    }

    private void Jump()
    {
        _rb.linearVelocityY = 0;
        _rb.AddForceY(jumpForce, ForceMode2D.Impulse);
        if(!_onGround.IsGrounded()) _animator.SetTrigger(JumpID);

        
    }
    
    private void TryJump(InputAction.CallbackContext _)
    {
        //if (NumJump == 0)
        // {
            // if (CanJump() == true)
            // {
                //Jump();
                //NumJump += 1;
                // }
                // } 

        //if (NumJump <= MaxJump)
        // {
            // Jump();
            // NumJump = 0;
            // }
            
            if (CanJump() && NumJump == 0)
            {
                Jump();
                NumJump++;
                return;
            }
            
            if (NumJump < MaxJump)
            {
                Jump();
                NumJump++;
                
            }

            if (NumJump == MaxJump)
            {
                if (CanJump())
                {
                    NumJump = 0;
                }
            }
    
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

    private void Update()
    {
        if (!useAnimation) return;
        _animator.SetFloat(MovementX, Mathf.Abs(_rb.linearVelocityX));
        _animator.SetBool(IsGrounded, _onGround.IsGrounded());
        
    }
    
    

    

}