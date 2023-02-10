using System;
using Remagures.Model.Interactable;
using Remagures.View.Character;

namespace Remagures.Model.Character
{
    public class CharacterInteractor
    {
        public InteractingState CurrentState { get; private set; }
        public IInteractable CurrentInteractable { get; private set; }
        
        private readonly ICharacterInteractorView _view;

        public CharacterInteractor(ICharacterInteractorView view)
            => _view = view ?? throw new ArgumentNullException(nameof(view));

        public void SetCurrentInteractable(IInteractable interactable)
        {
            if (CurrentInteractable != null)
                throw new InvalidOperationException("Interactor can't interacts with more than 1 interactable int the same time");

            CurrentInteractable = interactable ?? throw new ArgumentNullException(nameof(interactable));
            CurrentState = InteractingState.Ready;
        }

        public void Interact()
        {
            if (CurrentInteractable == null)
                throw new InvalidOperationException("Interactor can't interacts with null interactable");

            _player.ChangeState(PlayerState.Interact);
            CurrentState = InteractingState.Interact;
            CurrentInteractable.Interact();
        }

        public void EndInteraction()
        {
            _player.ChangeState(PlayerState.Idle);
            CurrentState = InteractingState.None;
            
            CurrentInteractable.EndInteracting();
            _view.DisplayEndOfInteraction();
            CurrentInteractable = null;
        }
    }
}