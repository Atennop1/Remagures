using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class AddQuestCallbackFactory : SerializedMonoBehaviour, IUsableComponentCallbackFactory
    {
        [SerializeField] private QuestsListFactory _questsListFactory;
        [SerializeField] private QuestFactory _questFactory;
        
        public void Create(IUsableComponent component)
        {
            var callback = new AddQuestCallback(component, _questsListFactory.Create(), _questFactory.Create());
        }
    }
}