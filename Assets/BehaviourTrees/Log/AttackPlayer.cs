using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Remagures.AI.Log 
{
    public class AttackPlayer : ActionNode
    {
        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override State OnUpdate() 
        {
            if (Vector3.Distance(context.enemyWithTarget.Target.position, context.transform.position) <= context.enemyWithTarget.AttackRadius && context.enemyWithTarget.CurrentState != EnemyState.Peace && context.enemyWithTarget.CurrentState != EnemyState.Stagger)
            {
                context.enemyWithTarget.StopMoveCoroutine();

                context.animator.SetBool("isStaying", true);
                Vector3 temp = Vector3.MoveTowards(context.transform.position, context.enemyWithTarget.Target.position, 1);
                context.enemyWithTarget.EnemyAnimations.ChangeAnim(temp - context.transform.position, context.animator);

                return State.Failure;
            }

            return State.Success;
        }
    }
}