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
        
        public void SetCharacterLookDirection(Vector2 direction)
        {
            foreach (var animator in _animators)
            {
                animator.SetFloat(DIRECTION_X_ANIMATION_HASH, direction.x);
                animator.SetFloat(DIRECTION_Y_ANIMATION_HASH, direction.y);
            }
        }

        public void SetBool(string key, bool value)
        {
            foreach (var animator in _animators)
                animator.SetBool(key, value);
        }
    }
}