using System;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public struct EnemyTargetData
    {
        public readonly Transform Transform;
        public readonly float AttackRadius;
        public readonly float ChaseRadius;

        public EnemyTargetData(Transform target, float attackRadius, float chaseRadius)
        {
            Transform = target ?? throw new ArgumentNullException(nameof(target));
            AttackRadius = attackRadius.ThrowExceptionIfLessOrEqualsZero();
            ChaseRadius = chaseRadius.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}