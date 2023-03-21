using Remagures.Model.DialogSystem;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class CompleteGoalCallbackFactory : MonoBehaviour, IUsableComponentCallbackFactory
    {
        [SerializeField] private GoalFactory _goalFactory;
        private GoalCompleting _builtCallback;
        
        public void Create(IUsableComponent component)
        {
            if (_builtCallback != null) 
                return;
            
            _builtCallback = new GoalCompleting(component, _goalFactory.Create());
        }
    }
}