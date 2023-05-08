using Remagures.Model.QuestSystem;

namespace Remagures.Root
{
    public interface IQuestsDatabaseFactory
    {
        QuestsDatabase Create();
    }
}