using Remagures.Model.QuestSystem;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class QuestsListFactory : MonoBehaviour, IQuestsListFactory
    {
        private IQuestsList _builtQuestsList;
        
        public IQuestsList Create()
        {
            if (_builtQuestsList != null)
                return _builtQuestsList;
            
            _builtQuestsList = new QuestsList();
            return _builtQuestsList;
        }
    }
}