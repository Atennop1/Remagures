using Remagures.Model.QuestSystem;
using UnityEngine.UI;

namespace Remagures.View.QuestSystem
{
    public interface IQuestSlotView
    {
        Button Button { get; }
        void Display(IQuest quest);
    }
}