using System;
using UnityEngine;

namespace Remagures.Model.Interactable
{
    public class PhysicsInteractable : MonoBehaviour, IInteractable
    {
        private IInteractable _interactable;

        public void Construct(IInteractable interactable)
            => _interactable = interactable ?? throw new ArgumentNullException(nameof(interactable));

        public void Interact()
            => _interactable.Interact();
    }
}