using System;
using Remagures.SO;
using Remagures.View.Chest;
using UnityEngine;

namespace Remagures.Model.Interactable
{
    public class PhysicsChest : MonoBehaviour, IChest
    {
        [SerializeField] private Signal _raiseItemSignal;
        [SerializeField] private Collider2D _triggerCollider;
        [SerializeField] private IChestView _chestView;

        public bool IsOpened => _chest.IsOpened;
        public BaseInventoryItem Item => _chest.Item;

        private IChest _chest;

        public void Construct(IChest chest)
        {
            _chest = chest ?? throw new ArgumentNullException(nameof(chest));
            _chestView.DisplayClosed();
            
            if (!_chest.IsOpened)
                return;
            
            _chestView.DisplayOpened();
            _triggerCollider.enabled = false;
        }

        public void Interact()
        {
            if (IsOpened)
                return;
            
            _chest.Interact();
            _chestView.DisplayItemName(_chest.Item.ItemDescription);
            
            _triggerCollider.enabled = false;
            _chestView.DisplayOpened();
            _raiseItemSignal.Invoke();
        }
    }
}