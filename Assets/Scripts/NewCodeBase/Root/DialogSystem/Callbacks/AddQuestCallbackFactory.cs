using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class AddQuestCallbackFactory : SerializedMonoBehaviour, IUsableComponentCallbackFactory
    {
        [SerializeField] private QuestsListFactory _questsListFactory;
        [SerializeField] private QuestFactory _questFactory;
        private QuestAdding _builtAdding;
        
        public void Create(IUsableComponent component)
        {
            if (_builtAdding != null)
                return;
            
            _builtAdding = new QuestAdding(component, _questsListFactory.Create(), _questFactory.Create());
        }
    }
}