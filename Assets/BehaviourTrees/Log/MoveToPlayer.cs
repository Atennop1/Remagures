using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Remagures.AI.Log 
{
    public class MoveToPlayer : ActionNode
    {
        public bool isAreaEnemy;

        protected override void OnStart()  { }

        protected override void OnStop()  { }

        protected override State OnUpdate() 
        {
            if (Vector3.Distance(context.enemyWithTarget.Target.position, context.transform.position) >= context.enemyWithTarget.AttackRadius && context.enemyWithTarget.CurrentState != EnemyState.Peace && context.enemyWithTarget.CurrentState != EnemyState.Stagger && (isAreaEnemy ? context.areaEnemy.Boundary.bounds.Contains(context.enemyWithTarget.Target.position) : true))
            {
                if (context.enemyWithTarget.CanFindPath)
                    context.enemyWithTarget.Move(context.enemyWithTarget.Target.position);
                
                context.animator.SetBool("wakeUp", true);
                context.animator.SetBool("isStaying", false);
                return State.Failure;
            }

            return State.Success;
        }
    }
}