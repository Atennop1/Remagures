using System;
using Remagures.Model.QuestSystem;
using Remagures.Tools;

namespace Remagures.Model.DialogSystem
{
    public sealed class GoalCompleting : IAdditionalBehaviour
    {
        private readonly IQuestGoal _questGoal;

        public GoalCompleting(IQuestGoal questGoal)
            => _questGoal = questGoal ?? throw new ArgumentNullException(nameof(questGoal));

        public void Use()
            => _questGoal.Complete();
    }
}