using Remagures.Model.QuestSystem.GoalListeners;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ShowPopupGoalListenerFactory : SerializedMonoBehaviour
    {
        [SerializeField] private IGoalFactory _goalFactory;
        [SerializeField] private IQuestFactory _questFactory;
        [SerializeField] private IQuestPopupsFactory questPopupsFactory;
        private readonly ISystemUpdate _systemUpdate = new SystemUpdate();

        private void Update()
            => _systemUpdate.UpdateAll();

        private void Awake()
        {
            var listener = new ShowPopupListener(_goalFactory.Create(), _questFactory.Create(), questPopupsFactory.Create());
            _systemUpdate.Add(listener);
        }
    }
}