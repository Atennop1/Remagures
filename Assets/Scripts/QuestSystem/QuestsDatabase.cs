using System.Linq;
using Remagures.SO.Other;
using Remagures.SO.PlayerStuff;
using Remagures.SO.QuestSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace Remagures.QuestSystem
{
    public class QuestsDatabase : MonoBehaviour
    {
        [SerializeField] private QuestContainer _container;
        [SerializeField] private Signal _labelSignal;
        [FormerlySerializedAs("_stringValue")] [SerializeField] private StringValue _labelQueuedTextStorage;

        public void AddQuestUI(Quest quest) => TryAddQuest(quest);
        
        public void TryCompleteGoal(QuestGoal completingGoal)
        {
            if (completingGoal.Quest.Completed) 
                return;
        
            completingGoal.AddAmount();
            TryAddQuest(completingGoal.Quest);

            foreach (var goal in completingGoal.Quest.Goals.Where(goal => goal.CanBeSkipped && 
                     completingGoal.Quest.Goals.IndexOf(goal) < completingGoal.Quest.Goals.IndexOf(completingGoal))) goal.AddAmount();

            if (TryDeleteQuest(completingGoal.Quest)) 
                return;
            
            _labelQueuedTextStorage.Value = "Квест обновлен: " + completingGoal.Quest.Information.Name;
            _labelSignal.Invoke();
        }
        
        private void TryAddQuest(Quest quest)
        {
            if (!_container.TryAddQuest(quest)) 
                return;
        
            _labelQueuedTextStorage.Value = "Новый квест: " + quest.Information.Name;
            _labelSignal.Invoke();
        }

        private bool TryDeleteQuest(Quest quest)
        {
            if (!_container.TryRemoveQuest(quest)) 
                return false;
            
            _labelQueuedTextStorage.Value = "Квест выполнен: " + quest.Information.Name;
            _labelSignal.Invoke();

            return true;
        }
    }
}
