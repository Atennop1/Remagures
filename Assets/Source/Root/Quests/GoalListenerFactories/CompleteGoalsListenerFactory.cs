using System.Linq;
using Remagures.Model.QuestSystem.GoalListeners;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class CompleteGoalsListenerFactory : MonoBehaviour
    {
        [SerializeField] private IGoalFactory _goalFactory;
        [SerializeField] private IGoalFactory[] _goalsToCompleteFactory;
        private readonly ISystemUpdate _systemUpdate = new SystemUpdate();

        private void Update()
            => _systemUpdate.UpdateAll();

        private void Awake()
        {
            var listener = new CompleteGoalsListener(_goalFactory.Create(), _goalsToCompleteFactory.Select(factory => factory.Create()).ToList());
            _systemUpdate.Add(listener);
        }
    }
}