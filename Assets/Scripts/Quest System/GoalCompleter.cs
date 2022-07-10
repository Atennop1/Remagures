using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCompleter : MonoBehaviour
{
    [SerializeField] private QuestsDatabase _database;
    [SerializeField] private QuestGoal _goal;

    public void Complete()
    {
        _database.TryCompleteGoal(_goal);
    }
}
