using System;
using Remagures.Model.DialogSystem;
using Remagures.Model.InventorySystem;
using Remagures.SO;

namespace Remagures.Model.Interactable
{
    public sealed class ChestWithDialogSwitching : IChest
    {
        public bool HasInteractionEnded => _chest.HasInteractionEnded;
        public bool IsOpened => _chest.IsOpened;
        public IItem Item => _chest.Item;
        
        private readonly IChest _chest;
        private readonly IDialogSwitcher _dialogSwitcher;

        public ChestWithDialogSwitching(IChest chest, IDialogSwitcher dialogSwitcher)
        {
            _chest = chest ?? throw new ArgumentNullException(nameof(chest));
            _dialogSwitcher = dialogSwitcher ?? throw new ArgumentNullException(nameof(dialogSwitcher));
        }
        
        public void Interact()
        {
            if (IsOpened)
                return;
            
            _chest.Interact();
            _dialogSwitcher.Switch();
        }

        public void OnInteractionEnd()
            => _chest.OnInteractionEnd();
    }
}