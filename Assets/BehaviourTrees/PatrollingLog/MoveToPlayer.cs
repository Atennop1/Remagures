using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Remagures.AI.PatrollingLog
{
    public class MoveToPlayer : ActionNode
    {
        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override State OnUpdate() 
        {
            if (Vector3.Distance(context.patrollingLog.Target.position, context.transform.position) > context.patrollingLog.AttackRadius && context.patrollingLog.CurrentState != EnemyState.Peace && context.enemyWithTarget.CurrentState != EnemyState.Stagger)
            {
                context.patrollingLog.Move(context.patrollingLog.Target.position);
                context.animator.SetBool("isStaying", false);
                return State.Failure;
            }

            return State.Success;
        }
    }
}