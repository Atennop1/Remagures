using System;
using Remagures.Root;
using Remagures.View.Interactable;

namespace Remagures.Model.AI.NPC
{
    public sealed class NPCInteractable : INPCInteractable, ILateUpdatable
    {
        public bool HasInteractionStarted { get; private set; } //TODO try to remove this
        public bool HasInteractionEnded { get; private set; }
        
        private readonly INPCInteractableView _npcInteractableView;

        public NPCInteractable(INPCInteractableView npcInteractableView) 
            => _npcInteractableView = npcInteractableView ?? throw new ArgumentNullException(nameof(npcInteractableView));

        public void LateUpdate()
        {
            HasInteractionStarted = false;
            HasInteractionEnded = false;
        }
        
        public void Interact()
        {
            HasInteractionStarted = true;
            _npcInteractableView.DisplayInteraction();
        }

        public void EndInteracting()
        {
            HasInteractionEnded = true;
            _npcInteractableView.DisplayEndOfInteraction();
        }
    }
}