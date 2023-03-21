using Remagures.Model.QuestSystem;

namespace Remagures.Tools
{
    public static class QuestGoalExtension
    {
        public static void Complete(this IQuestGoal goal) 
            => goal.Progress.AddPoints(goal.Progress.RequiredPoints - goal.Progress.CurrentPoints);
    }
}