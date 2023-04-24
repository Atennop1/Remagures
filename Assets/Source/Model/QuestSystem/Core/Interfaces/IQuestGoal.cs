namespace Remagures.Model.QuestSystem
{
    public interface IQuestGoal
    {
        string Description { get; }
        IProgress Progress { get; }

        bool IsCompleted { get; }
        bool HasCompleted { get; }
    }
}