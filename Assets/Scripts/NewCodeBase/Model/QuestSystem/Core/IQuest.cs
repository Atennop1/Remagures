using System.Collections.Generic;

namespace Remagures.Model.QuestSystem
{
    public interface IQuest
    {
        bool IsCompleted { get; }
        bool HasCompleted { get; }

        IReadOnlyList<IQuestGoal> Goals { get; }
        QuestData Data { get; }
    }
}