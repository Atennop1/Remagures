using System;
using Remagures.Model.AI.StateMachine;
using Remagures.Model.Input;

namespace Remagures.Model.Character
{
    public class MagicAttackStateSetuper : IStateSetuper
    {
        private readonly CharacterStates _states;
        private readonly CharacterAttacker _characterAttacker;
        private readonly IAttackInput _magicAttackInput;

        public MagicAttackStateSetuper(CharacterStates states, CharacterAttacker characterAttacker, IAttackInput magicAttackInput)
        {
            _states = states;
            _characterAttacker = characterAttacker ?? throw new ArgumentNullException(nameof(characterAttacker));
            _magicAttackInput = magicAttackInput ?? throw new ArgumentNullException(nameof(magicAttackInput));
        }
        
        public void Setup(StateMachine stateMachine)
        {
            stateMachine.AddTransition(_states.StandingState, _states.MagicAttackingState, () => _magicAttackInput.HasAttacked);
            stateMachine.AddTransition(_states.WalkingState, _states.MagicAttackingState, () => _magicAttackInput.HasAttacked);
            stateMachine.AddTransition(_states.MagicAttackingState, _states.StandingState, () => !_characterAttacker.IsAttacking);
        }
    }
}