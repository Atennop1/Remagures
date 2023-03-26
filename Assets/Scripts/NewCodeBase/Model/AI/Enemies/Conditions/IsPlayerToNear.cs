using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Remagures.Model.AI.Enemies.Conditions
{
    public sealed class IsPlayerToNear : Action
    {
        public SharedEnemyWithTarget SharedEnemyWithTarget;
        private IEnemyWithTarget _enemyWithTarget => SharedEnemyWithTarget.Value;
        
        public override TaskStatus OnUpdate()
        {
            return Vector3.Distance(_enemyWithTarget.TargetData.Transform.position,
                _enemyWithTarget.Movement.Transform.position) <= _enemyWithTarget.TargetData.AttackRadius
                ? TaskStatus.Success
                : TaskStatus.Failure;
        }
    }
}