using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalMarker : MonoBehaviour
{
    [field: SerializeField] public QuestGoal Goal { get; private set; }
    [SerializeField] private Image _marker;

    private QuestGoalsView _goalsView;

    public void Init(QuestGoalsView view)
    {
        _goalsView = view;
    }

    public void OpenGoals()
    {
        _goalsView.gameObject.SetActive(true);
        _goalsView.Initialize(Goal.Quest);
    }

    public bool IsGoalActive()
    {
        Quest quest = Goal.Quest;
        return Goal == quest.GetActiveGoal();
    }

    private void OnEnable()
    {
        _marker.gameObject.SetActive(false);
        if (!Goal.Completed)
        {
            if (IsGoalActive())
                _marker.gameObject.SetActive(true);
        }
    }
}
