using Remagures.Model.QuestSystem;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class GoalFactory : MonoBehaviour
    {
        [SerializeField] private GoalData _goalData;
        [SerializeField] private IProgressFactory _progressFactory;
        
        private QuestGoal _builtQuestGoal;
        private readonly ISystemUpdate _systemUpdate = new SystemUpdate();
        private readonly ILateSystemUpdate _lateSystemUpdate = new LateSystemUpdate();

        private void LateUpdate()
            => _lateSystemUpdate.UpdateAll();

        private void Update()
            => _systemUpdate.UpdateAll();
        
        public IQuestGoal Create()
        {
            if (_builtQuestGoal != null)
                return _builtQuestGoal;
            
            _builtQuestGoal = new QuestGoal(_goalData.Description, _progressFactory.Create());
            _systemUpdate.Add(_builtQuestGoal);
            _lateSystemUpdate.Add(_builtQuestGoal);
            return _builtQuestGoal;
        }
    }
}