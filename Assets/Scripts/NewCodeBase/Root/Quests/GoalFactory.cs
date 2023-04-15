using Remagures.Model.QuestSystem;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class GoalFactory : MonoBehaviour
    {
        [SerializeField] private GoalData _goalData;
        [SerializeField] private IProgressFactory _progressFactory;
        private IQuestGoal _builtQuestGoal;
        
        public IQuestGoal Create()
        {
            if (_builtQuestGoal != null)
                return _builtQuestGoal;
            
            _builtQuestGoal = new QuestGoal(_goalData.Description, _progressFactory.Create());
            return _builtQuestGoal;
        }
    }
}