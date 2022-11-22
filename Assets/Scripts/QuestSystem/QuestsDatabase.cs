using System.Linq;
using Remagures.SO.Other;
using Remagures.SO.PlayerStuff;
using Remagures.SO.QuestSystem;
using UnityEngine;

namespace Remagures.QuestSystem
{
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

        private void TryAddQuest(Quest quest)
        {
            if (!_container.TryAddQuest(quest)) return;
        
            _stringValue.Value = "Новый квест: " + quest.Information.Name;
            _labelSignal.Invoke();
        }

        public void TryCompleteGoal(QuestGoal completingGoal)
        {
            if (completingGoal.Quest.Completed) return;
        
            completingGoal.AddAmount();
            TryAddQuest(completingGoal.Quest);

            foreach (var goal in completingGoal.Quest.Goals.Where(goal => goal.CanBeSkipped && completingGoal.Quest.Goals.IndexOf(goal) < 
                         completingGoal.Quest.Goals.IndexOf(completingGoal))) goal.AddAmount();

            if (TryDeleteQuest(completingGoal.Quest)) return;
        
            if (_label.gameObject.activeInHierarchy)
            {
                _label.AddToQueue("Квест обновлен: " + completingGoal.Quest.Information.Name);
            }
            else
            {
                _stringValue.Value = "Квест обновлен: " + completingGoal.Quest.Information.Name;
                _labelSignal.Invoke();
            }
        }

        private bool TryDeleteQuest(Quest quest)
        {
            if (!_container.TryRemoveQuest(quest)) return false;
        
            if (_label.gameObject.activeInHierarchy)
            {
                _label.AddToQueue("Квест выполнен: " + quest.Information.Name);
            }
            else
            {
                _stringValue.Value = "Квест выполнен: " + quest.Information.Name;
                _labelSignal.Invoke();
            }
        
            return true;
        }
    }
}
