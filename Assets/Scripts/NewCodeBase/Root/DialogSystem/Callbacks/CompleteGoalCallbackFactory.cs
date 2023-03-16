using Remagures.Model.DialogSystem;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class CompleteGoalCallbackFactory : MonoBehaviour, IUsableComponentCallbackFactory
    {
        [SerializeField] private GoalFactory _goalFactory;
        private CompleteGoalCallback _builtCallback;
        
        public void Create(IUsableComponent component)
        {
            if (_builtCallback != null) 
                return;
            
            _builtCallback = new CompleteGoalCallback(component, _goalFactory.Create().Progress);
        }
    }
}