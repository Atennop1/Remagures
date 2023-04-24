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
        [SerializeField] private QuestFactory _questFactory;
        [SerializeField] private GoalFactory _goalFactory;
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