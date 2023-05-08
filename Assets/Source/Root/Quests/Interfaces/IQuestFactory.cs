using Remagures.Model.QuestSystem;

namespace Remagures.Root
{
    public interface IQuestFactory
    {
        int QuestID { get; }
        IQuest Create();
    }
}