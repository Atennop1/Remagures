using System.Collections;
using UnityEngine;

public class MeleeEnemy : EnemyWithTarget
{
    public bool CanAttack => _attackCoroutine == null;
    private Coroutine _attackCoroutine;

    public void StartAttackCoroutine()
    {
        if (_attackCoroutine != null)
            StopCoroutine(_attackCoroutine);
        _attackCoroutine = StartCoroutine(Attack());
    }

    private IEnumerator Attack()
    {
        EnemyAnimations.Animator.SetBool("isStaying", false);
        EnemyAnimations.Animator.SetBool("attacking", true);
        ChangeState(EnemyState.Attack);

        yield return new WaitForSeconds(0.35f);

        EnemyAnimations.Animator.SetBool("attacking", false);
        EnemyAnimations.Animator.SetBool("isStaying", true);
        ChangeState(EnemyState.None);
    }
}
