using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Remagures.AI.Ogre
{
    public class PlayerInRange : ActionNode
    {
        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override State OnUpdate() 
        {
            if (Vector3.Distance(context.meleeEnemy.Target.position, context.transform.position) > context.meleeEnemy.ChaseRadius)
            {
                context.meleeEnemy.StopMoveCoroutine();
                context.animator.SetBool("isStaying", true);
                return State.Failure;
            }

            return State.Success;
        }
    }
}
