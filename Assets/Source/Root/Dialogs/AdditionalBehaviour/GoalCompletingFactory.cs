using Remagures.Model.DialogSystem;
using UnityEngine;

namespace Remagures.Root.Dialogs
{
    public sealed class GoalCompletingFactory : MonoBehaviour, IAdditionalBehaviourFactory
    {
        [SerializeField] private GoalFactory _goalFactory;
        private IAdditionalBehaviour _builtBehaviour;
        
        public IAdditionalBehaviour Create()
        {
            if (_builtBehaviour != null) 
                return _builtBehaviour;
            
            _builtBehaviour = new GoalCompleting(_goalFactory.Create());
            return _builtBehaviour;
        }
    }
}