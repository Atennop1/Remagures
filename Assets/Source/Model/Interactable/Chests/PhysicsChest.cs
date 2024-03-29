﻿using System;
using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Model.Interactable
{
    public sealed class PhysicsChest : SerializedMonoBehaviour, IChest
    {
        [SerializeField] private Collider2D _triggerCollider;

        public bool HasInteractionEnded => _chest.HasInteractionEnded;
        public bool IsOpened => _chest.IsOpened;
        public IItem Item => _chest.Item;

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
        }

        public void OnInteractionEnd()
            => _chest.OnInteractionEnd();
    }
}