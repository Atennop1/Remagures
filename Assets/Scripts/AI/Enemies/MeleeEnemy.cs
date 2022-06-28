using System.Collections;
using UnityEngine;

public class MeleeEnemy : EnemyWithTarget
{
    public Coroutine AttackCoroutine;

    public IEnumerator Attack()
    {
        EnemyAnimations.Anim.SetBool("isStaying", false);
        EnemyAnimations.Anim.SetBool("attacking", true);

        yield return new WaitForSeconds(0.35f);

        EnemyAnimations.Anim.SetBool("attacking", false);
        EnemyAnimations.Anim.SetBool("isStaying", true);
    }
}
