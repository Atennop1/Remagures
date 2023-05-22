using Remagures.Model.QuestSystem.QuestListeners;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.QuestListenersFactories
{
    public sealed class DeleteFromQuestsListListenerFactory : SerializedMonoBehaviour
    {
        [SerializeField] private IQuestFactory _questFactory;
        [SerializeField] private QuestsListFactory _questsListFactory;
        private readonly ISystemUpdate _systemUpdate = new SystemUpdate();

        private void Update()
            => _systemUpdate.UpdateAll();

        private void Awake()
        {
            var listener = new DeleteFromQuestsListListener(_questFactory.Create(), _questsListFactory.Create());
            _systemUpdate.Add(listener);
        }
    }
}