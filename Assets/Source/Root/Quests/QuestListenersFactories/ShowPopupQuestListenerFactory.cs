using Remagures.Model.QuestSystem.QuestListeners;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.QuestListenersFactories
{
    public sealed class ShowPopupQuestListenerFactory : SerializedMonoBehaviour
    {
        [SerializeField] private IQuestFactory _questFactory;
        [SerializeField] private IQuestPopupsFactory questPopupsFactory;
        private readonly ISystemUpdate _systemUpdate = new SystemUpdate();

        private void Update()
            => _systemUpdate.UpdateAll();

        private void Awake()
        {
            var listener = new ShowPopupListener(_questFactory.Create(), questPopupsFactory.Create());
            _systemUpdate.Add(listener);
        }
    }
}