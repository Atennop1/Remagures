using System.Linq;
using Remagures.Model.QuestSystem.GoalListeners;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class CompleteGoalsListenerFactory : MonoBehaviour
    {
        [SerializeField] private GoalFactory _goalFactory;
        [SerializeField] private GoalFactory[] _goalsToCompleteFactory;

        private void Awake()
        {
            var listener = new CompleteGoalsListener(_goalFactory.Create(), _goalsToCompleteFactory.Select(factory => factory.Create()).ToList());
        }
    }
}