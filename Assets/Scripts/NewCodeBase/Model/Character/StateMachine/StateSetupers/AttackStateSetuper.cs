using System;
using Remagures.AI.StateMachine;
using Remagures.Model.Input;

namespace Remagures.Model.Character
{
    public class AttackStateSetuper : IStateSetuper
    {
        private readonly CharacterStates _states;
        private readonly CharacterAttacker _characterAttacker;
        private readonly IAttackInput _attackInput;

        public AttackStateSetuper(CharacterStates states, CharacterAttacker characterAttacker, IAttackInput attackInput)
        {
            _states = states;
            _characterAttacker = characterAttacker ?? throw new ArgumentNullException(nameof(characterAttacker));
            _attackInput = attackInput ?? throw new ArgumentNullException(nameof(attackInput));
        }

        public void Setup(StateMachine stateMachine)
        {
            stateMachine.AddTransition(_states.StandingState, _states.AttackingState, () => _attackInput.HasAttacked);
            stateMachine.AddTransition(_states.WalkingState, _states.AttackingState, () => _attackInput.HasAttacked);
            stateMachine.AddTransition(_states.AttackingState, _states.StandingState, () => !_characterAttacker.IsAttacking);
        }
    }
}