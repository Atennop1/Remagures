using Remagures.SO.QuestSystem;
using UnityEngine;

namespace Remagures.Quest_System
{
    public class GoalCompleter : MonoBehaviour
    {
        [SerializeField] private QuestsDatabase _database;
        [SerializeField] private QuestGoal _goal;

        public void Complete()
        {
            _database.TryCompleteGoal(_goal);
        }
    }
}
