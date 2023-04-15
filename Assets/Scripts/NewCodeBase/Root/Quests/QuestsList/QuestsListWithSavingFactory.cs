using System.Collections.Generic;
using Remagures.Model.QuestSystem;
using SaveSystem;
using SaveSystem.Paths;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class QuestsListWithSavingFactory : SerializedMonoBehaviour, IQuestsListFactory
    {
        [SerializeField] private IQuestsListFactory _questsListFactory;
        [SerializeField] private QuestsDatabaseFactory _questsDatabaseFactory;
        [SerializeField] private string _savePath;
        private IQuestsList _builtQuestsList;
        
        public IQuestsList Create()
        {
            if (_builtQuestsList != null)
                return _builtQuestsList;

            var storage = new BinaryStorage<List<int>>(new Path(_savePath));
            _builtQuestsList = new QuestsListWithSaving(_questsListFactory.Create(), storage, _questsDatabaseFactory.Create());
            return _builtQuestsList;
        }
    }
}