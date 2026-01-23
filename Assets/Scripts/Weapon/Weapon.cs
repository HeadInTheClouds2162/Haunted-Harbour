using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected abstract void Attack();
    [SerializeField] private float CooldownTime = 1;
    Coroutine _cooldownCoroutine;
    Coroutine _attackCoroutine;
    private bool _isAttacking;
    
    
    
    
    public void StartAttacking()
    {
        _isAttacking = true;
        if (_attackCoroutine is null)
            _attackCoroutine = StartCoroutine(AttackLoop());
        


    }

    public void StopAttacking()
    {
        _isAttacking = false;
    }
    
    

    private IEnumerator AttackLoop()
    {
        while (_isAttacking)
        {
            if (!IsOnCooldown())
            {
                _cooldownCoroutine = StartCoroutine(Cooldown());
                Attack();
            }
            yield return null;
        }

        _attackCoroutine = null;

    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(CooldownTime);
        _cooldownCoroutine = null;
    }

    bool IsOnCooldown()
    {
        return _cooldownCoroutine != null;
    }


}
