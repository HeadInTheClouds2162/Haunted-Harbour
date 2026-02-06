using System.Collections;
using UnityEngine;

[SelectionBase]
public abstract class Weapon : MonoBehaviour
{
    protected abstract void Attack();
    [SerializeField] private float cooldownTime = 0.2f;
    Coroutine _cooldownCoroutine;
    Coroutine _attackCoroutine;
    private bool _isAttacking;
    
    
    public void StartAttacking()
    {
        _isAttacking = true;
        if (_attackCoroutine is null)
        {
            _attackCoroutine = StartCoroutine(AttackLoop());
        }
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
        yield return new WaitForSeconds(cooldownTime);
        _cooldownCoroutine = null;
    }

    bool IsOnCooldown()
    {
        return _cooldownCoroutine != null;
    }


}
