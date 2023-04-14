using Remagures.Model.QuestSystem;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class QuestsListFactory : MonoBehaviour
    {
        [SerializeField] private QuestPopupsFactory questPopupsFactory;
        private IQuestsList _builtQuest;
        
        public IQuestsList Create()
        {
            if (_builtQuest != null)
                return _builtQuest;
            
            _builtQuest = new QuestsList(questPopupsFactory.Create());
            return _builtQuest;
        }
    }
}