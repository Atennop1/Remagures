using System.Linq;

namespace Remagures.Model.QuestSystem
{
    public sealed class ActiveGoalSelector
    {
        public IQuestGoal GetActiveGoalFor(IQuest quest) 
            => quest.Goals.First(goal => !goal.IsCompleted);
    }
}