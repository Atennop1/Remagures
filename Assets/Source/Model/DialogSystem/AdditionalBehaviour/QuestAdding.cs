using System;
using Remagures.Model.QuestSystem;

namespace Remagures.Model.DialogSystem
{
    public sealed class QuestAdding : IAdditionalBehaviour
    {
        private readonly IQuestsList _questsList;
        private readonly IQuest _quest;

        public QuestAdding(IQuestsList questsList, IQuest quest)
        {
            _questsList = questsList ?? throw new ArgumentNullException(nameof(questsList));
            _quest = quest ?? throw new ArgumentNullException(nameof(quest));
        }

        public void Use()
        {
            if (_questsList.CanAddQuest(_quest))
                _questsList.AddQuest(_quest);
        }
    }
}