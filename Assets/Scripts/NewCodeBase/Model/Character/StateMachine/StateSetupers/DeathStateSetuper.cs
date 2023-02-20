using System;
using Remagures.Model.AI;
using Remagures.Model.Health;

namespace Remagures.Model.Character
{
    public class DeathStateSetuper : IStateSetuper
    {
        private readonly CharacterStates _states;
        private readonly IHealth _playerHealth;

        public DeathStateSetuper(CharacterStates states, IHealth playerHealth)
        {
            _states = states;
            _playerHealth = playerHealth ?? throw new ArgumentNullException(nameof(playerHealth));
        }

        public void Setup(StateMachine stateMachine)
            => stateMachine.AddUniversalTransition(_states.DeathState, () => _playerHealth.IsDead);
    }
}