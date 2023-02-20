using Remagures.Model.DialogSystem;
using Remagures.Root;
using Remagures.View.Interactable;

namespace Remagures.Model.AI.NPC
{
    public class NPCInteractable : INPCInteractable, ILateUpdatable
    {
        public bool HasInteractionStarted { get; private set; }
        public bool HasInteractionEnded { get; private set; }
        
        private readonly INPCInteractableView _npcInteractableView;
        private readonly DialogView _dialogView; //TODO maybe i need to use polling instead pushing
        private readonly DialogsListRoot _dialogsListRoot;
        
        public void LateUpdate()
        {
            HasInteractionStarted = false;
            HasInteractionEnded = false;
        }
        
        public void Interact()
        {
            HasInteractionStarted = true;
            _dialogView.Activate(_dialogsListRoot.BuiltDialogList.CurrentDialog);
            _npcInteractableView.DisplayInteraction();
        }

        public void EndInteracting()
        {
            HasInteractionEnded = true;
            _npcInteractableView.DisplayEndOfInteraction();
        }
    }
}