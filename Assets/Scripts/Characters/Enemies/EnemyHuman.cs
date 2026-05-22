using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHuman : Enemy
{
    private delegate void DAiFunction();
    private DAiFunction currentFunction;
    private EAiState currentState;

    private WeaponController weaponController;
    private float _timer;
    private float Timetill;
    [SerializeField] private float mindirectionchange = 0.4f;
    [SerializeField] private float maxdirectionchange2 = 3f;
    private int ChooseNewDirectionHave;
    private float directionTimeChange;
    private float currentTimeChange;

    protected override void Die()
    {
        base.Die();
        weaponController.Shoot(false);
    }

    private void IdleState()
    {
        ///Timetill = Random.Range (5,10);
        ///  _timer  += Time.deltaTime;
        /// if (_timer >= Timetill)
        ///  {
        ///     SetState(EAiState.Searching);
        ///  }
        currentTimeChange += Time.deltaTime;
        if (currentTimeChange > maxdirectionchange2)
        {
            ChooseNewDirection();
        }
        
        rigidbody2D.AddForceX(ChooseNewDirectionHave * directionTimeChange);
    }

    private void ChooseNewDirection()
        {
            currentTimeChange = 0;
            directionTimeChange = Random.Range(mindirectionchange, maxdirectionchange2);
            ChooseNewDirectionHave = Random.Range(-1, 2);



        }

    private void SearchingState()
    {
        
    }

    private void AttackingState()
    {
        if (!_target) return;
        weaponController.PointHandTarget(_target.position);
       
    }

    private void FleeingState()
    {
        
    }

    protected override void Start()
    {
        
        weaponController = GetComponent<WeaponController>();

        Weapon[] weapons = GetComponentsInChildren<Weapon>();
        
        if(_target) SetState(EAiState.Attacking);
        else SetState(EAiState.Idle);
        
        weaponController.SetWeapon(weapons[Random.Range(0, weapons.Length)]); //<< CHOOSE A RANDOM WEAPON
        
        base.Start();
    }

    private void SetState(EAiState newState)
    {
        if(currentState != EAiState.Attacking)
            weaponController.Shoot(false);
        
        currentState = newState;
        
        if(newState == EAiState.Idle)
        {
            currentFunction = IdleState;
        }
        else if(newState == EAiState.Searching)
        {
            currentFunction = SearchingState;
        }
        else if(newState == EAiState.Attacking)
        {
            currentFunction = AttackingState;
            weaponController.Shoot(true);
        }
        else if(newState == EAiState.Fleeing)
        {
            currentFunction = FleeingState;
        }
        
    }

    protected override void Move()
    {
        currentFunction();
        bool lookDirection = _target ? transform.position.x < _target.position.x : rigidbody2D.linearVelocityX > 0;
        SetlookDirection(lookDirection);
    }
    

    protected override void OnNewTarget()
    {
        SetState(EAiState.Attacking);
    }
}

public enum EAiState
{
    Idle,
    Searching,
    Attacking,
    Fleeing
}
