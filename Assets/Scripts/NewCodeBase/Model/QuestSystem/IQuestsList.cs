namespace Remagures.Model.QuestSystem
{
    public interface IQuestsList
    {
        void AddQuest(IQuest quest);
        bool CanAddQuest(IQuest quest);

        void RemoveQuest(IQuest quest);
        bool CanRemoveQuest(IQuest quest);
    }
}