using Remagures.Model.QuestSystem.GoalListeners;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ActivateQuestGoalListenerFactory : MonoBehaviour
    {
        [SerializeField] private GoalFactory _goalFactory;
        [SerializeField] private QuestsListFactory _questsListFactory;
        [SerializeField] private QuestFactory _questToActivateFactory;

        private void Awake()
        {
            var listener = new ActivateQuestListener(_goalFactory.Create(), _questsListFactory.Create(), _questToActivateFactory.Create());
        }
    }
}