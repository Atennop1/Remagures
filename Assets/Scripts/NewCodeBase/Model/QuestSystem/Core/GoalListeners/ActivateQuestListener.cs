using System;
using Remagures.Root;

namespace Remagures.Model.QuestSystem.GoalListeners
{
    public sealed class ActivateQuestListener : IUpdatable
    {
        private readonly IQuestGoal _goal;
        private readonly IQuestsList _questsList;
        private readonly IQuest _questToActivate;

        public ActivateQuestListener(IQuestGoal goal, IQuestsList questsList, IQuest questToActivate)
        {
            _goal = goal ?? throw new ArgumentNullException(nameof(goal));
            _questsList = questsList ?? throw new ArgumentNullException(nameof(questsList));
            _questToActivate = questToActivate ?? throw new ArgumentNullException(nameof(questToActivate));
        }

        public void Update()
        {
            if (_goal.HasCompleted && _questsList.CanAddQuest(_questToActivate))
                _questsList.AddQuest(_questToActivate);
        }
    }
}