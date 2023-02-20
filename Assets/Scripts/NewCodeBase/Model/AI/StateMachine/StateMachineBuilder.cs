using System;
using System.Collections.Generic;

namespace Remagures.Model.AI
{
    public class StateMachineBuilder
    {
        private readonly List<IStateSetuper> _stateSetupers;

        public StateMachineBuilder(List<IStateSetuper> stateSetupers)
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