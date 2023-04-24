using System;
using System.Collections.Generic;
using System.Linq;
using SaveSystem;

namespace Remagures.Model.QuestSystem
{
    public sealed class QuestsListWithSaving : IQuestsList
    {
        public IReadOnlyList<IQuest> Quests => _questsList.Quests;
        
        private readonly IQuestsList _questsList;
        private readonly ISaveStorage<List<int>> _idsStorage;
        private readonly QuestsDatabase _questsDatabase;

        public QuestsListWithSaving(IQuestsList questsList, ISaveStorage<List<int>> idsStorage, QuestsDatabase questsDatabase)
        {
            _questsList = questsList ?? throw new ArgumentNullException(nameof(questsList));
            _idsStorage = idsStorage ?? throw new ArgumentNullException(nameof(idsStorage));
            _questsDatabase = questsDatabase ?? throw new ArgumentNullException(nameof(questsDatabase));

            foreach (var id in _idsStorage.Load()) 
                _questsList.AddQuest(_questsDatabase.GetByID(id));
        }

        public void AddQuest(IQuest quest)
        {
            _questsList.AddQuest(quest);
            _idsStorage.Save(_questsList.Quests.Select(questForSaving => _questsDatabase.GetQuestID(questForSaving)).ToList());
        }

        public bool CanAddQuest(IQuest quest) 
            => _questsList.CanAddQuest(quest);

        public void RemoveQuest(IQuest quest)
        {
            _questsList.RemoveQuest(quest);
            _idsStorage.Save(_questsList.Quests.Select(questForSaving => _questsDatabase.GetQuestID(questForSaving)).ToList());
        }

        public bool CanRemoveQuest(IQuest quest) 
            => _questsList.CanRemoveQuest(quest);
    }
}