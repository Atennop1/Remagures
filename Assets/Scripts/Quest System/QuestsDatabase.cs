using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestsDatabase : MonoBehaviour
{
    [SerializeField] private QuestContainer _container;
    [SerializeField] private QuestTextLabel _label;

    [Space]
    [SerializeField] private Signal _labelSignal;
    [SerializeField] private StringValue _stringValue;

    public void AddQuestUI(Quest quest)
    {
        TryAddQuest(quest);
    }

    public bool TryAddQuest(Quest quest)
    {
        if (_container.TryAddQuest(quest))
        {
            if (_label.gameObject.activeInHierarchy)
                _label.AddToQueue("Новый квест: " + quest.Information.Name);
            else
            {
                _stringValue.Value = "Новый квест: " + quest.Information.Name;
                _labelSignal.Invoke();
            }

            return true;
        }

        return false;
    }

    public void TryCompleteGoal(QuestGoal goal)
    {
        if (!goal.Quest.Completed)
        {
            goal.AddAmount();
            if (goal.Completed)
            {
                TryAddQuest(goal.Quest);

                for (int i = 0; i < goal.Quest.Goals.Count; i++)
                    if (goal.Quest.Goals[i].CanBeSkipped)
                        goal.Quest.Goals[i].AddAmount();

                if (!TryDeleteQuest(goal.Quest))
                {
                    if (_label.gameObject.activeInHierarchy)
                        _label.AddToQueue("Квест обновлен: " + goal.Quest.Information.Name);
                    else
                    {
                        _stringValue.Value = "Квест обновлен: " + goal.Quest.Information.Name;
                        _labelSignal.Invoke();
                    }
                }
            }
        }
    }

    private bool TryDeleteQuest(Quest quest)
    {
        if (_container.TryRemoveQuest(quest))
        {
            if (_label.gameObject.activeInHierarchy)
                _label.AddToQueue("Квест выполнен: " + quest.Information.Name);
            else
            {
                _stringValue.Value = "Квест выполнен: " + quest.Information.Name;
                _labelSignal.Invoke();
            }
            return true;
        }

        return false;
    }
}
