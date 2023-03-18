using System;
using Remagures.Model.InventorySystem;
using Remagures.SO;
using Remagures.View.Interactable;

namespace Remagures.Model.Interactable
{
    public sealed class ChestWithView : IChest
    {
        public bool HasInteractionEnded => _chest.HasInteractionEnded;
        public bool IsOpened => _chest.IsOpened;
        public IItem Item => _chest.Item;

        private readonly IChest _chest;
        private readonly IChestView _chestView;

        public ChestWithView(IChest chest, IChestView chestView)
        {
            _chest = chest ?? throw new ArgumentNullException(nameof(chest));
            _chestView = chestView ?? throw new ArgumentNullException(nameof(chestView));
            
            _chestView.DisplayClosed();
            
            if (_chest.IsOpened)
                _chestView.DisplayOpened();
        }

        public void Interact()
        {
            if (IsOpened)
                return;
            
            _chest.Interact();
            _chestView.DisplayItemName(_chest.Item.Description);
            _chestView.DisplayOpened();
        }

        public void EndInteracting() 
            => _chest.EndInteracting();
    }
}