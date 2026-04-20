using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyHuman : Enemy
{
    private delegate void DAiFunction();
    
    private DAiFunction currentFunction;
    private EAiState currentState;

    private WeaponController weaponController;
    
    private Transform target;
    
    
    private void IdleState()
    {
        
    }

    private void SearchingState()
    {
        
    }

    private void AttackingState()
    {
        
    }

    private void FleeingState()
    {
        
    }

    private void Start()
    {
        SetState(EAiState.Idle);
        weaponController = GetComponent<WeaponController>();

        Weapon[] weapons = GetComponentsInChildren<Weapon>();
        weaponController.SetWeapon(weapons[Random.Range(0, weapons.Length)]); //<< CHOOSE A RANDOM WEAPON
    }

    private void SetState(EAiState newState)
    {
        if(currentState == EAiState.Attacking)
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
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Player p))
        {
            target = p.transform;
            SetState(EAiState.Attacking);
        }
    }
}

public enum EAiState
{
    Idle,
    Searching,
    Attacking,
    Fleeing
}
