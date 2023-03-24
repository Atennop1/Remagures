using System;
using Remagures.Model.InventorySystem;
using SaveSystem;

namespace Remagures.Model.Interactable
{
    public sealed class ChestWithSaving : IChest
    {
        public bool HasInteractionEnded => _chest.HasInteractionEnded;
        public bool IsOpened { get; private set; }
        public IItem Item => _chest.Item;

        private readonly IChest _chest;
        private readonly ISaveStorage<bool> _isOpenedStorage;

        public ChestWithSaving(IChest chest, ISaveStorage<bool> saveStorage)
        {
            _chest = chest ?? throw new ArgumentNullException(nameof(chest));
            _isOpenedStorage = saveStorage ?? throw new ArgumentNullException(nameof(saveStorage));
            IsOpened = _isOpenedStorage.Load();
        }

        public void Interact()
        {
            if (IsOpened)
                return;
            
            _chest.Interact();
            _isOpenedStorage.Save(true);
            IsOpened = true;
        }

        public void EndInteracting() 
            => _chest.EndInteracting();
    }
}