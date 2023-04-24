using System;
using System.Collections.Generic;
using System.Linq;
using Remagures.Root;
using Remagures.Tools;
using Sirenix.Utilities;

namespace Remagures.Model.QuestSystem.GoalListeners
{
    public sealed class CompleteGoalsListener : IUpdatable
    {
        private readonly IQuestGoal _goal;
        private readonly List<IQuestGoal> _goalsToComplete;

        public CompleteGoalsListener(IQuestGoal goal, List<IQuestGoal> goalsToComplete)
        {
            _goal = goal ?? throw new ArgumentNullException(nameof(goal));
            _goalsToComplete = goalsToComplete ?? throw new ArgumentNullException(nameof(goalsToComplete));
        }

        public void Update()
        {
            if (_goal.HasCompleted)
                _goalsToComplete.ForEach(goal => goal.Complete());
        }
    }
}