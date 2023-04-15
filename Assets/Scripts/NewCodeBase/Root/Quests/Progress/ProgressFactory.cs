using Remagures.Model.QuestSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ProgressFactory : SerializedMonoBehaviour, IProgressFactory
    {
        [SerializeField] private GoalData _goalData;
        private IProgress _builtProgress;
        
        public IProgress Create()
        {
            if (_builtProgress != null)
                return _builtProgress;

            _builtProgress = new Progress(_goalData.RequiredPointsCount);
            return _builtProgress;
        }
    }
}