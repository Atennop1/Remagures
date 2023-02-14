using System;
using Remagures.Model.Health;
using Remagures.Root;
using Remagures.Tools;

namespace Remagures.Model.InventorySystem
{
    public class AddHealthListener : IUpdatable
    {
        private readonly IUsableItem _usableItem;
        private readonly IHealth _health;
        private readonly int _amountToIncrease;

        public AddHealthListener(IUsableItem usableItem, IHealth health, int amountToIncrease)
        {
            _usableItem = usableItem ?? throw new ArgumentNullException(nameof(usableItem));
            _health = health ?? throw new ArgumentNullException(nameof(health));
            _amountToIncrease = amountToIncrease.ThrowExceptionIfLessOrEqualsZero();
        }

        public void Update()
        {
            if (_usableItem.HasUsed)
                _health.Heal(_amountToIncrease);
        }
    }
}
