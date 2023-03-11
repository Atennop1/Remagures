using Remagures.Model.QuestSystem.QuestListeners;
using UnityEngine;

namespace Remagures.Root.QuestListenersFactories
{
    public sealed class ShowPopupQuestListenerFactory : MonoBehaviour
    {
        [SerializeField] private QuestFactory _questFactory;
        [SerializeField] private QuestPopupsFactory questPopupsFactory;

        private void Awake()
        {
            var listener = new ShowPopupListener(_questFactory.Create(), questPopupsFactory.Create());
        }
    }
}