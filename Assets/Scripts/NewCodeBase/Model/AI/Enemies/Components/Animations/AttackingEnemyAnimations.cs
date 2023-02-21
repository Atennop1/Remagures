using System;
using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public class AttackingEnemyAnimations : IAttackingEnemyAnimations
    {
        private readonly IEnemyAnimations _enemyAnimations;
        private readonly Animator _animator;
        
        private readonly int IS_ATTACKING_ANIMATION_HASH = Animator.StringToHash("attacking");

        public AttackingEnemyAnimations(IEnemyAnimations enemyAnimations, Animator animator)
        {
            _enemyAnimations = enemyAnimations ?? throw new ArgumentNullException(nameof(enemyAnimations));
            _animator = animator ?? throw new ArgumentNullException(nameof(animator));
        }

        public void SetIsAttacking(bool isActive) 
            => _animator.SetBool(IS_ATTACKING_ANIMATION_HASH, isActive);
        
        public void SetIsDead(bool isActive)
            => _enemyAnimations.SetIsDead(isActive);

        public void SetIsKnocked(bool isActive)
            => _enemyAnimations.SetIsKnocked(isActive);

        public void SetAnimationsVector(Vector2 vector) 
            => _enemyAnimations.SetAnimationsVector(vector);

        public void SetIsWakeUp(bool isActive) 
            => _enemyAnimations.SetIsWakeUp(isActive);

        public void SetIsStaying(bool isActive) 
            => _enemyAnimations.SetIsStaying(isActive);
    }
}