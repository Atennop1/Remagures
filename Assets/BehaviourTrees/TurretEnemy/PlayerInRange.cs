using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Remagures.AI.TurretLog
{
    public class PlayerInRange : ActionNode
    {
        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override State OnUpdate() 
        {
            if (Vector3.Distance(context.turretEnemy.Target.position, context.transform.position) >= context.turretEnemy.ChaseRadius)
            {
                context.animator.SetBool("wakeUp", false);
                context.animator.SetBool("isStaying", true);

                return State.Failure;
            }
            
            return State.Success;
        }
    }
}