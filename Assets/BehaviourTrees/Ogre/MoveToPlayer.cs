using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Remagures.AI.Ogre
{
    public class MoveToPlayer : ActionNode
    {
        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override State OnUpdate() 
        {
            if (Vector3.Distance(context.meleeEnemy.Target.position, context.transform.position) >= context.meleeEnemy.AttackRadius && context.meleeEnemy.CurrentState == EnemyState.None)
            {
                context.meleeEnemy.Move(context.meleeEnemy.Target);
                context.animator.SetBool("isStaying", false);
                return State.Failure;
            }

            return State.Success;
        }
    }
}