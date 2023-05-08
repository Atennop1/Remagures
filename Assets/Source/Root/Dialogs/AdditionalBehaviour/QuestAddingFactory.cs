using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Dialogs
{
    public sealed class QuestAddingFactory : SerializedMonoBehaviour, IAdditionalBehaviourFactory
    {
        [SerializeField] private QuestsListFactory _questsListFactory;
        [SerializeField] private IQuestFactory _questFactory;
        private IAdditionalBehaviour _builtBehaviour;
        
        public IAdditionalBehaviour Create()
        {
            if (_builtBehaviour != null)
                return _builtBehaviour;
            
            _builtBehaviour = new QuestAdding(_questsListFactory.Create(), _questFactory.Create());
            return _builtBehaviour;
        }
    }
}