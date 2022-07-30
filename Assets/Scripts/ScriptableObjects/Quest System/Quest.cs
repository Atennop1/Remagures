using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class Quest : ScriptableObject
{
    public QuestInfo Information;
    public List<QuestGoal> Goals;

    [field: SerializeField] public bool Completed { get; private set; }

    public void Initialize()
    {
        Completed = false;

        foreach (QuestGoal goal in Goals)
        {
            goal.Initialize();
            goal.GoalCompleted.AddListener(delegate { CheckGoals(); });
            goal.Evaluate();
        }
    }

    public QuestGoal GetActiveGoal()
    {
        if (!Completed)
            return Goals[Goals.FindLastIndex(x => x.Completed) + 1];
        
        throw new System.ArgumentException("Can't find active goal in completed quest!");
    }

    private void CheckGoals()
    {
        Completed = Goals.All(g => g.Completed);
    }

    [System.Serializable]
    public struct QuestInfo
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public Sprite QuestSprite { get; private set; }
    }
}