using System;
using Remagures.Model.Interactable;
using Remagures.View.Character;

namespace Remagures.Model.Character
{
    public sealed class CharacterInteractor : ICharacterInteractor
    {
        public InteractionState CurrentState { get; private set; }
        public IInteractable CurrentInteractable { get; private set; }
        
        private readonly ICharacterInteractorView _view;

        public CharacterInteractor(ICharacterInteractorView view)
            => _view = view ?? throw new ArgumentNullException(nameof(view));
        
        public void Interact()
        {
            if (CurrentInteractable == null)
                throw new InvalidOperationException("Interactor can't interacts with null interactable");

            if (CurrentState != InteractionState.Ready)
                throw new InvalidOperationException("Interaction can be started only from \"Ready\" state");
            
            CurrentState = InteractionState.Active;
            CurrentInteractable.Interact();
        }

        public void SetCurrentInteractable(IInteractable interactable)
        {
            if (CurrentInteractable != null)
                throw new InvalidOperationException("Interactor can't interacts with more than 1 interactable int the same time");

            CurrentInteractable = interactable ?? throw new ArgumentNullException(nameof(interactable));
            CurrentState = InteractionState.Ready;
        }

        public void EndInteraction()
        {
            CurrentState = InteractionState.None;
            CurrentInteractable.EndInteracting();
            
            _view.DisplayEndOfInteraction();
            CurrentInteractable = null;
        }
    }
}