using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected abstract void Attack();
    [SerializeField] float CooldownTime = 4;
    Coroutine _cooldownCoroutine;
    Coroutine _attackCoroutine;
    private bool _isAttacking;
    public void StartAttacking()
    {
        
    }

    public void StopAttacking()
    {
        
    }

    private IEnumerator AttackLoop()
    {
        while (_isAttacking)
        {
            _cooldownCoroutine = StartCoroutine(Cooldown());
            yield return null;
        }
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(CooldownTime);
    }

    bool IsOnCooldown()
    {
        return _cooldownCoroutine != null;
    }


}
