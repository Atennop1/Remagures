using System;
using Remagures.Root;

namespace Remagures.Model.QuestSystem.GoalListeners
{
    public sealed class ShowPopupListener : IUpdatable
    {
        private readonly IQuestGoal _goal;
        private readonly IQuest _quest;
        private readonly QuestPopups _questPopups;

        public ShowPopupListener(IQuestGoal goal, IQuest quest, QuestPopups questPopups)
        {
            _goal = goal ?? throw new ArgumentNullException(nameof(goal));
            _quest = quest ?? throw new ArgumentNullException(nameof(quest));
            _questPopups = questPopups ?? throw new ArgumentNullException(nameof(questPopups));
        }

        public void Update()
        {
            if (_quest.IsCompleted || !_goal.HasCompleted)
                return;
            
            _questPopups.AddTextToQueue("Квест обновлен: " + _quest.Data.Name);
        }
    }
}