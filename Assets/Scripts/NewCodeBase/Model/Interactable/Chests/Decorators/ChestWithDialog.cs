using System;
using Remagures.Model.DialogSystem;
using Remagures.Model.InventorySystem;

namespace Remagures.Model.Interactable
{
    public sealed class ChestWithDialog : IChest
    {
        public bool HasInteractionEnded => _chest.HasInteractionEnded && _dialogPlayer.HasPlayed;
        public bool IsOpened => _chest.IsOpened;
        public IItem Item => _chest.Item;
        
        private readonly IChest _chest;
        private readonly IDialogPlayer _dialogPlayer;
        private readonly IDialog _dialog;

        public ChestWithDialog(IChest chest, IDialogPlayer dialogPlayer, IDialog dialog)
        {
            _chest = chest ?? throw new ArgumentNullException(nameof(chest));
            _dialogPlayer = dialogPlayer ?? throw new ArgumentNullException(nameof(dialogPlayer));
            _dialog = dialog ?? throw new ArgumentNullException(nameof(dialog));
        }

        public void Interact()
        {
            _dialogPlayer.Play(_dialog);
            _chest.Interact();
        }

        public void OnInteractionEnd() 
            => _chest.OnInteractionEnd();
    }
}