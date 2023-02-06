using System.Linq;
using Remagures.SO;
using UnityEngine;
using UnityEngine.Serialization;

namespace Remagures.QuestSystem
{
    public class QuestContainerOperations : MonoBehaviour
    {
        [SerializeField] private QuestContainer _container;
        [SerializeField] private QuestTextLabel _label;
        
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
            
            _label.AddToQueue("Квест обновлен: " + completingGoal.Quest.Information.Name);
        }
        
        public void TryAddQuest(Quest quest)
        {
            if (!_container.TryAddQuest(quest)) 
                return;
        
            _label.AddToQueue("Новый квест: " + quest.Information.Name);
        }

        private bool TryDeleteQuest(Quest quest)
        {
            if (!_container.TryRemoveQuest(quest)) 
                return false;
            
            _label.AddToQueue("Квест выполнен: " + quest.Information.Name);
            return true;
        }
    }
}
