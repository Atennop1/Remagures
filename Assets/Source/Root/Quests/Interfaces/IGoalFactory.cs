using Remagures.Model.QuestSystem;

namespace Remagures.Root
{
    public interface IGoalFactory
    {
        IQuestGoal Create();
    }
}