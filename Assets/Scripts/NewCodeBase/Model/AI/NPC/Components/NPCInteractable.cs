using Remagures.Model.DialogSystem;
using Remagures.Model.Interactable;
using Remagures.Root;
using Remagures.View.Interactable;

namespace Remagures.Model.AI.NPC
{
    public class NPCInteractable : IInteractable
    {
        public bool HasInteracted { get; private set; }
        private readonly INPCInteractableView _npcInteractableView;
        
        private readonly DialogView _dialogView; //TODO maybe i need to use polling instead pushing
        private readonly DialogsListRoot _dialogsListRoot;
        
        public void Interact()
        {
            HasInteracted = true;
            _dialogView.Activate(_dialogsListRoot.BuiltDialogList.CurrentDialog);
            _npcInteractableView.DisplayInteraction();
        }

        public void EndInteracting()
        {
            HasInteracted = false;
            _npcInteractableView.DisplayEndOfInteraction();
        }
    }
}