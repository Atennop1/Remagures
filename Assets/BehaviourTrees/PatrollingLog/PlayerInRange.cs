using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Remagures.AI.PatrollingLog
{
    public class PlayerInRange : ActionNode
    {
        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override State OnUpdate() 
        {
            if (Vector3.Distance(context.patrollingLog.Target.position, context.transform.position) > context.patrollingLog.ChaseRadius || context.patrollingLog.CurrentState == EnemyState.Peace)
            {
                if (Vector3.Distance(context.transform.position, context.patrollingLog.Path[context.patrollingLog.CurrentPoint].position) > 0.1f)
                {
                    context.patrollingLog.Move(context.patrollingLog.Path[context.patrollingLog.CurrentPoint]);
                    context.animator.SetBool("isStaying", false);
                }
                else
                    context.patrollingLog.ChangeGoal();

                return State.Failure;
            }

            return State.Success;
        }
    }
}
