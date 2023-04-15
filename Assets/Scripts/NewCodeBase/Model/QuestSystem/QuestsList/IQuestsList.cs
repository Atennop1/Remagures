using System.Collections.Generic;

namespace Remagures.Model.QuestSystem
{
    public interface IQuestsList
    {
        IReadOnlyList<IQuest> Quests { get; }
        void AddQuest(IQuest quest);
        bool CanAddQuest(IQuest quest);

        void RemoveQuest(IQuest quest);
        bool CanRemoveQuest(IQuest quest);
    }
}