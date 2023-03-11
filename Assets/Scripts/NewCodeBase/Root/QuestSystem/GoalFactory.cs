using Remagures.Model.QuestSystem;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class GoalFactory : MonoBehaviour
    {
        [SerializeField] private GoalData _goalData;
        private IQuestGoal _builtQuestGoal;
        
        public IQuestGoal Create()
        {
            if (_builtQuestGoal != null)
                return _builtQuestGoal;
            
            var progress = new Progress(_goalData.RequiredPointsCount);
            _builtQuestGoal = new QuestGoal(_goalData.Description, progress);
            return _builtQuestGoal;
        }
    }
}