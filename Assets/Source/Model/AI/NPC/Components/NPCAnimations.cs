using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Model.AI.NPC
{
    public sealed class NPCAnimations : SerializedMonoBehaviour
    {
        private readonly int MOVE_X_ANIMATION_HASH = Animator.StringToHash("moveX");
        private readonly int MOVE_Y_ANIMATION_HASH = Animator.StringToHash("moveY");
        private readonly int IS_STAYING_ANIMATION_HASH = Animator.StringToHash("isStaying");
        
        private readonly Animator _animator;

        public NPCAnimations(Animator animator)
            => _animator = animator ?? throw new ArgumentNullException(nameof(animator));

        public void ActivateIsStaying()
            => _animator.SetBool(IS_STAYING_ANIMATION_HASH, true);
        
        public void DeactivateIsStaying()
            => _animator.SetBool(IS_STAYING_ANIMATION_HASH, false);

        public void SetAnimationsVector(Vector2 vector)
        {
            _animator.SetFloat(MOVE_X_ANIMATION_HASH, vector.x);
            _animator.SetFloat(MOVE_Y_ANIMATION_HASH, vector.y);
        }
    }
}
