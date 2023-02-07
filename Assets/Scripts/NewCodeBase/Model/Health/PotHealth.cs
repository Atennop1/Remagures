using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Remagures.Model.Health
{
    public sealed class PotHealth : IHealth
    {
        public int MaxValue => _health.MaxValue;
        public int CurrentValue => _health.CurrentValue;

        public bool IsDead => _health.IsDead;
        public bool CanTakeDamage => _health.CanTakeDamage;
        
        private readonly IHealth _health = new Health(1);
        private readonly int SMASH_ANIMATION_HASH = Animator.StringToHash("Smash");
        
        private readonly Animator _potAnimator;
        private readonly GameObject _objectInPot;

        public PotHealth(Animator potAnimator, GameObject objectInPot)
        {
            _potAnimator = potAnimator ?? throw new ArgumentNullException(nameof(potAnimator));
            _objectInPot = objectInPot ?? throw new ArgumentNullException(nameof(objectInPot));
        }

        public void TakeDamage(int amount)
        {
            _health.TakeDamage(1);
            _potAnimator.Play(SMASH_ANIMATION_HASH);
            Object.Instantiate(_objectInPot, _potAnimator.transform.position, Quaternion.identity);
        }

        public void Heal(int amount) { }
    }
}