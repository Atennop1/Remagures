using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class QuestAddingFactory : SerializedMonoBehaviour, IAdditionalBehaviourFactory
    {
        [SerializeField] private QuestsListFactory _questsListFactory;
        [SerializeField] private QuestFactory _questFactory;
        private QuestAdding _builtBehaviour;
        
        public IAdditionalBehaviour Create()
        {
            if (_builtBehaviour != null)
                return _builtBehaviour;
            
            _builtBehaviour = new QuestAdding(_questsListFactory.Create(), _questFactory.Create());
            return _builtBehaviour;
        }
    }
}