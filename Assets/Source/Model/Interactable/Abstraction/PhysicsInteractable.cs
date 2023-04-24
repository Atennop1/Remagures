using System;
using UnityEngine;

namespace Remagures.Model.Interactable
{
    public sealed class PhysicsInteractable : MonoBehaviour, IInteractable
    {
        public bool HasInteractionEnded => _interactable.HasInteractionEnded;
        
        private IInteractable _interactable;
        
        public void Construct(IInteractable interactable)
            => _interactable = interactable ?? throw new ArgumentNullException(nameof(interactable));

        public void Interact()
            => _interactable.Interact();

        public void OnInteractionEnd()
            => _interactable.OnInteractionEnd();
    }
}