using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class AddQuestCallbackFactory : SerializedMonoBehaviour, IUsableComponentCallbackFactory
    {
        [SerializeField] private QuestsListFactory _questsListFactory;
        [SerializeField] private QuestFactory _questFactory;
        private AddQuestCallback _builtCallback;
        
        public void Create(IUsableComponent component)
        {
            if (_builtCallback != null)
                return;
            
            _builtCallback = new AddQuestCallback(component, _questsListFactory.Create(), _questFactory.Create());
        }
    }
}