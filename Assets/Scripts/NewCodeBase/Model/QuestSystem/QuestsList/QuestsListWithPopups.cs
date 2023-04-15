using System;
using System.Collections.Generic;

namespace Remagures.Model.QuestSystem
{
    public sealed class QuestsListWithPopups : IQuestsList
    {
        public IReadOnlyList<IQuest> Quests => _questsList.Quests;
        
        private readonly IQuestsList _questsList;
        private readonly QuestPopups _questPopups;

        public QuestsListWithPopups(IQuestsList questsList, QuestPopups questPopups)
        {
            _questsList = questsList ?? throw new ArgumentNullException(nameof(questsList));
            _questPopups = questPopups ?? throw new ArgumentNullException(nameof(questPopups));
        }

        public void AddQuest(IQuest quest)
        {
            _questsList.AddQuest(quest);
            _questPopups.AddTextToQueue("Новый квест: " + quest.Data.Name);
        }

        public bool CanAddQuest(IQuest quest) 
            => _questsList.CanAddQuest(quest);

        public void RemoveQuest(IQuest quest) 
            => _questsList.RemoveQuest(quest);

        public bool CanRemoveQuest(IQuest quest) 
            => _questsList.CanRemoveQuest(quest);
    }
}