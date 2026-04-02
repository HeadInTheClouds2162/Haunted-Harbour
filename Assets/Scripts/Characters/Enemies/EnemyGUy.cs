using UnityEngine;

public class EnemyGUy : Enemy
{
    private delegate void DAiFunction();
    private DAiFunction currentFunction;
    private EAiState currentState;
    private WeaponController weaponController;
    private Transform _player;
    private void Start()
    {
        SetState(EAiState.Idle);
        weaponController = GetComponent<WeaponController>();
        Weapon[]weapons = GetComponentsInChildren<Weapon>();
        weaponController.SetWeapon(weapons[Random.Range (0, weapons.Rank)]);
    }
    protected override void Move()
    {
        currentFunction();
    }

    private void SetState(EAiState newState)
    {
        if (currentState == EAiState.Attacking)
        {
            weaponController.Shoot(false);
        }
        currentState = newState;
        if (newState == EAiState.Idle)
        {
            currentState = EAiState.Idle;
        }
        else if (newState == EAiState.Searching)
        {
            currentFunction = MoveState;
        }
        else if (newState == EAiState.Attacking)
        {
            currentFunction = AttackState;
            weaponController.Shoot(true);
        }
        else if (newState == EAiState.Fleeing)
        {
            currentFunction = FleeState;
        }
    }

    private void MoveState()
    {
        
    }

    private void IdleState()
    {
        
    }

    private void AttackState()
    {
        
    }

    private void FleeState()
    {
        
    }

    public enum EAiState
    {
        Idle,
        Searching,
        Attacking,
        Fleeing,
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Player target))
        {
            _player = target.transform;
            SetState(EAiState.Attacking);
        }
    }

}
