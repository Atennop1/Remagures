using System;
using System.Collections.Generic;
using System.Linq;
using Remagures.Root;

namespace Remagures.Model.QuestSystem
{
    public sealed class Quest : IQuest, IUpdatable, ILateUpdatable
    {
        public bool IsCompleted { get; private set; }
        public bool HasCompleted { get; private set; }
        
        public QuestData Data { get; }
        public IReadOnlyList<IQuestGoal> Goals => _goals;

        private readonly List<QuestGoal> _goals;

        public Quest(List<QuestGoal> goals, QuestData questData)
        {
            Data = questData;
            _goals = goals ?? throw new ArgumentNullException(nameof(goals));
        }

        public void Update()
        {
            if (!_goals.Any(goal => goal.HasCompleted) || IsCompleted)
                return;

            IsCompleted = _goals.All(goal => goal.IsCompleted);

            if (IsCompleted)
                HasCompleted = true;
        }

        public void LateUpdate()
            => HasCompleted = false;
    }
}