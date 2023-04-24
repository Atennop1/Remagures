using System;
using Remagures.Model.DialogSystem;
using Remagures.Root.Dialogs;

namespace Remagures.Model.AI.NPC
{
    public sealed class NPCInteractableWithDialogPlaying : INPCInteractable
    {
        public bool HasInteractionEnded => _npcInteractable.HasInteractionEnded;
        public bool HasInteractionStarted => _npcInteractable.HasInteractionStarted;
        
        private readonly INPCInteractable _npcInteractable;
        private readonly IDialogPlayer _dialogPlayer;
        private readonly IDialogs _dialogs;

        public NPCInteractableWithDialogPlaying(INPCInteractable npcInteractable, IDialogPlayer dialogPlayer, IDialogs dialogs)
        {
            _npcInteractable = npcInteractable ?? throw new ArgumentNullException(nameof(npcInteractable));
            _dialogPlayer = dialogPlayer ?? throw new ArgumentNullException(nameof(dialogPlayer));
            _dialogs = dialogs ?? throw new ArgumentNullException(nameof(dialogs));
        }

        public void Interact()
        {
            _dialogPlayer.Play(_dialogs.CurrentDialog);
            _npcInteractable.Interact();
        }

        public void OnInteractionEnd() 
            => _npcInteractable.OnInteractionEnd();
    }
}