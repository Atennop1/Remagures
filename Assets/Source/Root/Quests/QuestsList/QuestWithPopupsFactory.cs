using Remagures.Model.QuestSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class QuestWithPopupsFactory : SerializedMonoBehaviour, IQuestsListFactory
    {
        [SerializeField] private IQuestsListFactory _questsListFactory;
        [SerializeField] private IQuestPopupsFactory questPopupsFactory;
        private IQuestsList _builtQuestsList;
        
        public IQuestsList Create()
        {
            if (_builtQuestsList != null)
                return _builtQuestsList;
            
            _builtQuestsList = new QuestsListWithPopups(_questsListFactory.Create(), questPopupsFactory.Create());
            return _builtQuestsList;
        }
    }
}