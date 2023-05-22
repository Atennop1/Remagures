using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.View.Pot
{
    public sealed class PotView : SerializedMonoBehaviour, IPotView
    {
        [SerializeField] private Animator _potAnimator;
        
        private readonly int SMASH_ANIMATION_HASH = Animator.StringToHash("Smash");

        public void PlaySmashAnimation()
            => _potAnimator.Play(SMASH_ANIMATION_HASH);
    }
}