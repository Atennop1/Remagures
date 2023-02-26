using Remagures.Root;

namespace Remagures.Model.QuestSystem.GoalListeners
{
    public sealed class ShowPopupListener : IUpdatable
    {
        private readonly IQuestGoal _goal;
        private readonly IQuest _quest;
        private readonly QuestPopupTexts _questPopupTexts;
        
        public void Update()
        {
            if (_quest.IsCompleted || !_goal.HasCompleted)
                return;
            
            _questPopupTexts.AddTextToPopupQueue("Квест обновлен: " + _quest.Data.Name);
        }
    }
}