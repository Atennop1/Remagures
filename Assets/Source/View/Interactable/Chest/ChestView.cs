using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.View.Interactable
{
    public sealed class ChestView : SerializedMonoBehaviour, IChestView
    {
        [SerializeField] private Animator _animator;

        private readonly int OPENED_ANIMATION_HASH = Animator.StringToHash("opened");

        public void DisplayClosed()
            => _animator.SetBool(OPENED_ANIMATION_HASH, true);

        public void DisplayOpened()
            => _animator.SetBool(OPENED_ANIMATION_HASH, true);
    }
}