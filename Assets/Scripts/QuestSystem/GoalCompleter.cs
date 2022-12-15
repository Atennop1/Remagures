using Remagures.SO.QuestSystem;
using UnityEngine;

namespace Remagures.QuestSystem
{
    public class GoalCompleter : MonoBehaviour
    {
        [SerializeField] private QuestContainerOperations _database;
        [SerializeField] private QuestGoal _goal;

        public void Complete()
        {
            _database.TryCompleteGoal(_goal);
        }
    }
}
