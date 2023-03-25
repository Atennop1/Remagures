using System;
using Remagures.Model.Health;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public sealed class ItemWithHealthAdding : IUsableItem
    {
        public string Name => _usableItem.Name;
        public string Description => _usableItem.Description;
        public Sprite Sprite => _usableItem.Sprite;
        public bool IsStackable => _usableItem.IsStackable;
        
        private readonly IUsableItem _usableItem;
        private readonly IHealth _health;
        private readonly int _amountToIncrease;

        public ItemWithHealthAdding(IUsableItem usableItem, IHealth health, int amountToIncrease)
        {
            _usableItem = usableItem ?? throw new ArgumentNullException(nameof(usableItem));
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _amountToIncrease = amountToIncrease.ThrowExceptionIfLessOrEqualsZero();
        }

        public void Use()
        {
            _usableItem.Use();
            _health.Heal(_amountToIncrease);
        }
    }
}
