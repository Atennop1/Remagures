﻿using System;
using Remagures.Root;

namespace Remagures.Model.QuestSystem.QuestListeners
{
    public sealed class DeleteFromQuestsListListener : IUpdatable
    {
        private readonly IQuest _quest;
        private readonly IQuestsList _questsList;

        public DeleteFromQuestsListListener(IQuest quest, IQuestsList questsList)
        {
            _quest = quest ?? throw new ArgumentNullException(nameof(quest));
            _questsList = questsList ?? throw new ArgumentNullException(nameof(questsList));
        }

        public void Update()
        {
            if (_quest.HasCompleted && _questsList.CanRemoveQuest(_quest))
                _questsList.RemoveQuest(_quest);
        }
    }
}