using System.Linq;
using Remagures.Model.QuestSystem;

namespace Remagures.Tools
{
    public static class QuestExtensions
    {
        public static IQuestGoal GetActiveGoal(this IQuest quest)
            => quest.Goals.First(goal => !goal.IsCompleted);
    }
}