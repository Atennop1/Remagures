using Remagures.Model.MapSystem;
using Sirenix.OdinInspector;

namespace Remagures.Root
{
    public sealed class IsGoalMarkerContainsInMapFactory : SerializedMonoBehaviour, IIsMarkerContainsInMapFactory
    {
        private IIsMarkerContainsInMap _builtCondition;

        public IIsMarkerContainsInMap Create()
        {
            if (_builtCondition != null)
                return _builtCondition;

            _builtCondition = new IsGoalMarkerContainsInMap();
            return _builtCondition;
        }
    }
}