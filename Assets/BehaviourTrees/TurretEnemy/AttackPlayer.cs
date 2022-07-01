using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

namespace Remagures.AI.TurretLog
{
    public class AttackPlayer : ActionNode
    {
        protected override void OnStart() { }

        protected override void OnStop() { }

        protected override State OnUpdate() 
        {
            if (context.turretEnemy.CanFire && context.enemyWithTarget.CurrentState != EnemyState.Stagger)
            {
                Vector3 tempVector = context.turretEnemy.Target.transform.position - context.transform.position;
                context.turretEnemy.InstantiateProjectile(tempVector);

                context.turretEnemy.EnemyAnimations.ChangeAnim(tempVector, context.animator);
                    
                context.animator.SetBool("wakeUp", true);
                context.animator.SetBool("isStaying", true);

                return State.Failure;
            }

            return State.Success;
        }
    }
}