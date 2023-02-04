using System.Collections.Generic;
using UnityEngine;

namespace Remagures.SO
{
    [CreateAssetMenu(menuName = "Quest System/QuestContainer")]
    public class QuestContainer : ScriptableObject
    {
        public IReadOnlyList<Quest> Quests => _quests;
        [SerializeField] private List<Quest> _quests;

        public bool TryAddQuest(Quest quest)
        {
            if (_quests.Contains(quest) || quest.Completed) return false;
        
            _quests.Add(quest);
            return true;

        }

        public bool TryRemoveQuest(Quest quest)
        {
            if (!quest.Completed) return false;
        
            _quests.Remove(quest);
            return true;

        }

        public void Reset()
            => _quests.Clear();
    }
}
