using System;
using Remagures.Model.QuestSystem;
using Remagures.Root;

namespace Remagures.Model.DialogSystem
{
    public sealed class CompleteGoalCallback : IUpdatable
    {
        private readonly IUsableComponent _usableComponent;
        private readonly IProgress _goalProgress;
        
        private bool _hasWorked;

        public CompleteGoalCallback(IUsableComponent usableComponent, IProgress goalProgress)
        {
            _usableComponent = usableComponent ?? throw new ArgumentNullException(nameof(usableComponent));
            _goalProgress = goalProgress ?? throw new ArgumentNullException(nameof(goalProgress));
        }

        public void Update()
        {
            if (!_usableComponent.IsUsed || _hasWorked) 
                return;
            
            _goalProgress.AddPoints(_goalProgress.RequiredPoints - _goalProgress.CurrentPoints);
            _hasWorked = true;
        }
    }
}