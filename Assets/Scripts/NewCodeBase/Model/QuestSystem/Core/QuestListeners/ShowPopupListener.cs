using System;
using Remagures.Root;

namespace Remagures.Model.QuestSystem
{
    public sealed class ShowPopupListener : IUpdatable
    {
        private readonly IQuest _quest;
        private readonly QuestPopupTexts _questPopupTexts;

        public ShowPopupListener(IQuest quest, QuestPopupTexts questPopupTexts)
        {
            _quest = quest ?? throw new ArgumentNullException(nameof(quest));
            _questPopupTexts = questPopupTexts ?? throw new ArgumentNullException(nameof(questPopupTexts));
        }

        public void Update()
        {
            if (_quest.HasCompleted)
                _questPopupTexts.AddTextToPopupQueue("Квест выполнен: " + _quest.Data.Name);
        }
    }
}