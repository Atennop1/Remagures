using System;
using Remagures.Model.QuestSystem;
using Remagures.Root;
using Remagures.Tools;

namespace Remagures.Model.DialogSystem
{
    public sealed class CompleteGoalCallback : IUpdatable
    {
        private readonly IUsableComponent _usableComponent;
        private readonly IQuestGoal _questGoal;
        
        private bool _hasWorked;

        public CompleteGoalCallback(IUsableComponent usableComponent, IQuestGoal questGoal)
        {
            _usableComponent = usableComponent ?? throw new ArgumentNullException(nameof(usableComponent));
            _questGoal = questGoal ?? throw new ArgumentNullException(nameof(questGoal));
        }

        public void Update()
        {
            if (!_usableComponent.IsUsed || _hasWorked) 
                return;
            
            _questGoal.Complete();
            _hasWorked = true;
        }
    }
}