using Remagures.Model.InventorySystem;
using UnityEngine;

namespace Remagures.View.Inventory
{
    public sealed class DisplayableItemView : MonoBehaviour, IDisplayableItemView
    {
        [SerializeField] private Animator _itemAnimator;

        public void Display(IDisplayableItem displayableItem)
            => _itemAnimator.runtimeAnimatorController = displayableItem.AnimatorController;
    }
}