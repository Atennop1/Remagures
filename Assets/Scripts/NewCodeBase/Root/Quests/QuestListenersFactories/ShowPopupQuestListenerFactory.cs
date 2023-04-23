using Remagures.Model.QuestSystem.QuestListeners;
using UnityEngine;

namespace Remagures.Root.QuestListenersFactories
{
    public sealed class ShowPopupQuestListenerFactory : MonoBehaviour
    {
        [SerializeField] private QuestFactory _questFactory;
        [SerializeField] private QuestPopupsFactory questPopupsFactory;
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