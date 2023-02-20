using System;
using UnityEngine;

namespace Remagures.Model.AI.Enemies
{
    public class EnemyAnimations
    {
        public Animator Animator { get; }

        private readonly int MOVE_X_ANIMATION_HASH = Animator.StringToHash("moveX");
        private readonly int MOVE_Y_ANIMATION_HASH = Animator.StringToHash("moveY");
        
        private readonly int WAKE_UP_ANIMATION_HASH = Animator.StringToHash("wakeUp");
        private readonly int IS_STAYING_ANIMATION_HASH = Animator.StringToHash("isStaying");
        
        public EnemyAnimations(Animator animator)
            => Animator = animator ?? throw new ArgumentNullException(nameof(animator));

        public void SetAnimationsVector(Vector2 vector)
        {
            Animator.SetFloat(MOVE_X_ANIMATION_HASH, vector.x);
            Animator.SetFloat(MOVE_Y_ANIMATION_HASH, vector.y);
        }
        
        public void SetIsWakeUp(bool isActive)
            => Animator.SetBool(WAKE_UP_ANIMATION_HASH, isActive);

        public void SetIsStaying(bool isActive)
            => Animator.SetBool(IS_STAYING_ANIMATION_HASH, isActive);

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
