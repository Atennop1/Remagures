using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest System/QuestContainer")]
public class QuestContainer : ScriptableObject
{
    public IReadOnlyList<Quest> Quests => _quests;
    [SerializeField] private List<Quest> _quests;

    public bool TryAddQuest(Quest quest)
    {
        if (!_quests.Contains(quest) && !quest.Completed)
        {
            _quests.Add(quest);
            return true;
        }

        return false;
    }

    public bool TryRemoveQuest(Quest quest)
    {
        if (quest.Completed)
        {
            _quests.Remove(quest);
            return true;
        }

        return false;
    }

    public void Reset()
    {
        _quests.Clear();
    }
}
