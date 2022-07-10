using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public sealed class QuestGoal : ScriptableObject
{
    [field: SerializeField] public Quest Quest { get; private set; }
    [field: SerializeField] public string Description { get; private set; }

    [field: SerializeField, Space] public int CurrentAmount { get; private set; }
    [field: SerializeField] public int RequiredAmount { get; private set; }

    [field: SerializeField] public bool CanBeSkipped { get; private set; }

    public bool Completed { get; private set; }
    public UnityEvent GoalCompleted { get; private set; }

    public void Initialize()
    {
        Completed = false;
        GoalCompleted = new UnityEvent();
    }

    public void AddAmount()
    {
        if (CurrentAmount < RequiredAmount)
        {
            CurrentAmount++;
            Evaluate();
        }
    }

    public void Reset()
    {
        CurrentAmount = 0;
    }

    public void Evaluate()
    {
        if (CurrentAmount >= RequiredAmount)
            Complete();
    }

    private void Complete()
    {
        Completed = true;
        GoalCompleted?.Invoke();
        GoalCompleted?.RemoveAllListeners();
    }
}