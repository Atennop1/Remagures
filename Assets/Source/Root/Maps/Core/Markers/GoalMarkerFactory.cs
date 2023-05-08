using Remagures.Model.MapSystem;
using Remagures.View.MapSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class GoalMarkerFactory : SerializedMonoBehaviour, IMarkerFactory
    {
        [SerializeField] private IMarkerView _markerView;
        [SerializeField] private QuestsListFactory _questsListFactory;
        [SerializeField] private IQuestFactory _questFactory;
        [SerializeField] private IGoalFactory _goalFactory;
        private IMarker _builtMarker;
        
        public IMarker Create()
        {
            if (_builtMarker != null)
                return _builtMarker;

            _builtMarker = new GoalMarker(_markerView, _questsListFactory.Create(), _questFactory.Create(), _goalFactory.Create());
            return _builtMarker;
        }
    }
}