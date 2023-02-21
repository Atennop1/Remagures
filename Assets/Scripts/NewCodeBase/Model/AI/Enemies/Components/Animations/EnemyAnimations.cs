using System;
using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public sealed class EnemyAnimations : IEnemyAnimations
    {
        private readonly int MOVE_X_ANIMATION_HASH = Animator.StringToHash("moveX");
        private readonly int MOVE_Y_ANIMATION_HASH = Animator.StringToHash("moveY");
        
        private readonly int IS_WAKED_UP_ANIMATION_HASH = Animator.StringToHash("wakeUp");
        private readonly int IS_STAYING_ANIMATION_HASH = Animator.StringToHash("isStaying");
        
        private readonly int IS_DEAD_ANIMATION_HASH = Animator.StringToHash("dead");
        private readonly int IS_KNOCKED_ANIMATION_HASH = Animator.StringToHash("knocked");
        
        private readonly Animator _animator;

        public EnemyAnimations(Animator animator)
            => _animator = animator ?? throw new ArgumentNullException(nameof(animator));

        public void SetAnimationsVector(Vector2 vector)
        {
            _animator.SetFloat(MOVE_X_ANIMATION_HASH, vector.x);
            _animator.SetFloat(MOVE_Y_ANIMATION_HASH, vector.y);
        }
        
        public void SetIsDead(bool isActive)
            => _animator.SetBool(IS_DEAD_ANIMATION_HASH, isActive);

        public void SetIsKnocked(bool isActive)
            => _animator.SetBool(IS_KNOCKED_ANIMATION_HASH, isActive);
        
        public void SetIsWakeUp(bool isActive)
            => _animator.SetBool(IS_WAKED_UP_ANIMATION_HASH, isActive);

        public void SetIsStaying(bool isActive)
            => _animator.SetBool(IS_STAYING_ANIMATION_HASH, isActive);

            //public void ChangeAnim(Vector2 direction) //TODO decide what to do with it
        //{
        //    if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        //    {
        //        SetAnimationsVector(direction.x > 0 ? Vector2.right : Vector2.left);
        //    }
        //    else
        //    {
        //        SetAnimationsVector(direction.y > 0 ? Vector2.up : Vector2.down);
        //    }
        //}
    }
}
