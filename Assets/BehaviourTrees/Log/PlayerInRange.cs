using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Remagures.AI.Log 
{
    public class PlayerInRange : ActionNode
    {
        public bool isAreaEnemy;

        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override State OnUpdate() 
        {
            if (Vector3.Distance(context.enemyWithTarget.Target.position, context.transform.position) >= context.enemyWithTarget.ChaseRadius || context.enemyWithTarget.CurrentState == EnemyState.Peace || (isAreaEnemy ? !context.areaEnemy.Boundary.bounds.Contains(context.enemyWithTarget.Target.position) : false))
            {
                context.enemyWithTarget.StopMoveCoroutine();
                context.animator.SetBool("wakeUp", false);
                context.animator.SetBool("isStaying", false);
                return State.Failure;
            }

            return State.Success;
        }
    }
}