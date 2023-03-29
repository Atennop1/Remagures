using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Remagures.Model.Character.BehaviourTree
{
    public sealed class InteractingNode : Action
    {
        public SharedCharacterInteractor SharedCharacterInteractor;
        public SharedRigidbody2D SharedCharacterRigidbody;

        public void OnEnter()
        {
            SharedCharacterRigidbody.Value.constraints = RigidbodyConstraints2D.FreezeAll;
            SharedCharacterInteractor.Value.Interact();
        }
        
        public void OnExit()
            => SharedCharacterRigidbody.Value.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}