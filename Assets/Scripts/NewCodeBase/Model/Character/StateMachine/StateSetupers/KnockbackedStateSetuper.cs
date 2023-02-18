using System;
using Remagures.AI.StateMachine;
using Remagures.Model.Knockback;

namespace Remagures.Model.Character
{
    public class KnockbackedStateSetuper : IStateSetuper
    {
        private readonly CharacterStates _states;
        private readonly IKnockable _characterKnockable;

        public KnockbackedStateSetuper(CharacterStates states, IKnockable characterKnockable)
        {
            _states = states;
            _characterKnockable = characterKnockable ?? throw new ArgumentNullException(nameof(characterKnockable));
        }

        public void Setup(StateMachine stateMachine)
        {
            stateMachine.AddAnyTransition(_states.KnockbackedState, () => _characterKnockable.IsKnocking);
            stateMachine.AddExceptionToAnyTransition(_states.KnockbackedState, _states.DeathState, _states.InteractingState);
            stateMachine.AddTransition(_states.KnockbackedState, _states.StandingState, () => !_characterKnockable.IsKnocking);
        }
    }
}