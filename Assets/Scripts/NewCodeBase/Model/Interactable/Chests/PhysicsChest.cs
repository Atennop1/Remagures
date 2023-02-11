using System;
using Remagures.SO;
using UnityEngine;

namespace Remagures.Model.Interactable
{
    public sealed class PhysicsChest : MonoBehaviour, IChest
    {
        [SerializeField] private Signal _raiseItemSignal;
        [SerializeField] private Collider2D _triggerCollider;

        public bool HasInteracted => _chest.HasInteracted;
        public bool IsOpened => _chest.IsOpened;
        public BaseInventoryItem Item => _chest.Item;

        private IChest _chest;

        public void Construct(IChest chest)
        {
            _chest = chest ?? throw new ArgumentNullException(nameof(chest));

            if (_chest.IsOpened)
                _triggerCollider.enabled = false;
        }

        public void Interact()
        {
            if (IsOpened)
                return;
            
            _chest.Interact();
            _triggerCollider.enabled = false;
            _raiseItemSignal.Invoke();
        }

        public void EndInteracting()
            => _chest.EndInteracting();
    }
}