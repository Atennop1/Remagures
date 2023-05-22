using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.View.Inventory
{
    public sealed class DisplayableItemView : SerializedMonoBehaviour, IDisplayableItemView
    {
        [SerializeField] private Animator _itemAnimator;

        public void Display(IDisplayableItem displayableItem)
            => _itemAnimator.runtimeAnimatorController = displayableItem.AnimatorController;
    }
}