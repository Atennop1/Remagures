using System;
using System.Collections.Generic;
using System.Linq;
using Remagures.Root;

namespace Remagures.Model.CutscenesSystem
{
    public sealed class Cutscene : ICutscene, IUpdatable
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished => _actions.All(action => action.IsFinished);
        private ICutsceneAction _currentAction => _actions[_currentActionIndex];
        
        private readonly List<ICutsceneAction> _actions;
        private int _currentActionIndex;
        
        public void Start() 
            => IsStarted = true;
        
        public Cutscene(List<ICutsceneAction> actions) => 
            _actions = actions ?? throw new ArgumentException("Actions can't be null");
        
        public void Update()
        {
            if (!_currentAction.IsStarted)
                 _currentAction.Start();

            if (!_currentAction.IsFinished) 
                return;
            
            _currentAction.Finish();
            if (_currentActionIndex != _actions.Count - 1)
                _currentActionIndex++;

            if (IsFinished)
                IsStarted = false;
        }
    }
}