using Remagures.Model.QuestSystem.GoalListeners;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ActivateQuestGoalListenerFactory : SerializedMonoBehaviour
    {
        [SerializeField] private IGoalFactory _goalFactory;
        [SerializeField] private QuestsListFactory _questsListFactory;
        [SerializeField] private IQuestFactory _questToActivateFactory;
        private readonly ISystemUpdate _systemUpdate = new SystemUpdate();

        private void Update()
            => _systemUpdate.UpdateAll();

        private void Awake()
        {
            var listener = new ActivateQuestListener(_goalFactory.Create(), _questsListFactory.Create(), _questToActivateFactory.Create());
            _systemUpdate.Add(listener);
        }
    }
}