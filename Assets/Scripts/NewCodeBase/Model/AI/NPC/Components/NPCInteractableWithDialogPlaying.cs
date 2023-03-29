using System;
using Remagures.Model.DialogSystem;
using Remagures.Root.DialogSystem;

namespace Remagures.Model.AI.NPC
{
    public sealed class NPCInteractableWithDialogPlaying : INPCInteractable
    {
        public bool HasInteractionEnded => _npcInteractable.HasInteractionEnded;
        public bool HasInteractionStarted => _npcInteractable.HasInteractionStarted;
        
        private readonly INPCInteractable _npcInteractable;
        private readonly DialogPlayer _dialogPlayer;
        private readonly DialogsListFactory _dialogsListFactory;

        public NPCInteractableWithDialogPlaying(INPCInteractable npcInteractable, DialogPlayer dialogPlayer, DialogsListFactory dialogsListFactory)
        {
            _npcInteractable = npcInteractable ?? throw new ArgumentNullException(nameof(npcInteractable));
            _dialogPlayer = dialogPlayer ?? throw new ArgumentNullException(nameof(dialogPlayer));
            _dialogsListFactory = dialogsListFactory ?? throw new ArgumentNullException(nameof(dialogsListFactory));
        }

        public void Interact()
        {
            _dialogPlayer.Play(_dialogsListFactory.Create().CurrentDialog);
            _npcInteractable.Interact();
        }

        public void EndInteracting() 
            => _npcInteractable.EndInteracting();
    }
}