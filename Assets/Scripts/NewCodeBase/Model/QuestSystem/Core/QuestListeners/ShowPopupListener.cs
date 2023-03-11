using System;
using Remagures.Root;

namespace Remagures.Model.QuestSystem.QuestListeners
{
    public sealed class ShowPopupListener : IUpdatable
    {
        private readonly IQuest _quest;
        private readonly QuestPopups _questPopups;

        public ShowPopupListener(IQuest quest, QuestPopups questPopups)
        {
            _quest = quest ?? throw new ArgumentNullException(nameof(quest));
            _questPopups = questPopups ?? throw new ArgumentNullException(nameof(questPopups));
        }

        public void Update()
        {
            if (_quest.HasCompleted)
                _questPopups.AddTextToQueue("Квест выполнен: " + _quest.Data.Name);
        }
    }
}