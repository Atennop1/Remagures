using System;
using Remagures.AI.StateMachine;
using Remagures.Model.Input;

namespace Remagures.Model.Character
{
    public class InteractingStateSetuper : IStateSetuper
    {
        private readonly CharacterStates _states;
        private readonly ICharacterInteractor _characterInteractor;
        private readonly IInteractingInput _interactingInput;

        public InteractingStateSetuper(CharacterStates states, ICharacterInteractor characterInteractor, IInteractingInput interactingInput)
        {
            _states = states;
            _characterInteractor = characterInteractor ?? throw new ArgumentNullException(nameof(characterInteractor));
            _interactingInput = interactingInput ?? throw new ArgumentNullException(nameof(interactingInput));
        }

        public void Setup(StateMachine stateMachine)
        {
            stateMachine.AddTransition(_states.StandingState, _states.InteractingState, () => _interactingInput.HasInteracted);
            stateMachine.AddTransition(_states.WalkingState, _states.InteractingState, () => _interactingInput.HasInteracted);
            stateMachine.AddTransition(_states.MagicAttackingState, _states.StandingState, () => _characterInteractor.CurrentState != InteractionState.Active);
        }
    }
}