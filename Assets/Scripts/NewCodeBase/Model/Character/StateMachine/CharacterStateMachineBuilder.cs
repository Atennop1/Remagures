using System;
using System.Collections.Generic;
using Remagures.Model.AI.StateMachine;

namespace Remagures.Model.Character
{
    public class CharacterStateMachineBuilder
    {
        private readonly List<IStateSetuper> _stateSetupers;

        public CharacterStateMachineBuilder(List<IStateSetuper> stateSetupers)
            => _stateSetupers = stateSetupers ?? throw new ArgumentNullException(nameof(stateSetupers));

        public StateMachine Build()
        {
            var stateMachine = new StateMachine();

            foreach (var stateSetuper in _stateSetupers)
                stateSetuper.Setup(stateMachine);
                
            return stateMachine;
        }
    }
}