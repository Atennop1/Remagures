using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Remagures.AI.Ogre
{
    public class AttackPlayer : ActionNode
    {
        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override State OnUpdate() 
        {
            if (Vector3.Distance(context.meleeEnemy.Target.position, context.transform.position) <= context.meleeEnemy.AttackRadius && context.meleeEnemy.CurrentState != EnemyState.Peace && context.enemyWithTarget.CurrentState != EnemyState.Stagger)
            {
                context.meleeEnemy.StopMoveCoroutine();
                if (context.meleeEnemy.AttackCoroutine != null)
                    context.meleeEnemy.StopCoroutine(context.meleeEnemy.AttackCoroutine);
                context.meleeEnemy.AttackCoroutine = context.meleeEnemy.StartCoroutine(context.meleeEnemy.Attack());
                
                return State.Failure;
            }

            return State.Success;
        }
    }
}