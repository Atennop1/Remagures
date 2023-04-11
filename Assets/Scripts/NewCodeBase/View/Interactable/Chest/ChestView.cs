using UnityEngine;

namespace Remagures.View.Interactable
{
    public class ChestView : MonoBehaviour, IChestView
    {
        [SerializeField] private Animator _animator;

        private readonly int OPENED_ANIMATION_HASH = Animator.StringToHash("opened");

        public void DisplayClosed()
            => _animator.SetBool(OPENED_ANIMATION_HASH, true);

        public void DisplayOpened()
            => _animator.SetBool(OPENED_ANIMATION_HASH, true);
    }
}