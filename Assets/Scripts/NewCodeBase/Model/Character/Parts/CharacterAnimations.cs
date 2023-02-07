using System;
using System.Collections.Generic;
using UnityEngine;

namespace Remagures.Model.Character
{
    public sealed class CharacterAnimations : ICharacterAnimations
    {
        private readonly List<Animator> _animators;
        
        private readonly int DIRECTION_X_ANIMATION_HASH = Animator.StringToHash("DirectionX");
        private readonly int DIRECTION_Y_ANIMATION_HASH = Animator.StringToHash("DirectionY");

        public CharacterAnimations(List<Animator> animators)
            => _animators = animators ?? throw new ArgumentNullException(nameof(animators));
        
        public void SetAnimationsVector(Vector2 vector)
        {
            foreach (var animator in _animators)
            {
                animator.SetFloat(DIRECTION_X_ANIMATION_HASH, vector.x);
                animator.SetFloat(DIRECTION_Y_ANIMATION_HASH, vector.y);
            }
        }

        public void SetAnim(string key)
        {
            foreach (var animator in _animators)
            {
                animator.Play(key);
                animator.Play(key);
            }
        }
    }
}