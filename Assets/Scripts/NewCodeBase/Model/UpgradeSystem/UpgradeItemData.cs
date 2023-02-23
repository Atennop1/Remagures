using System;
using Remagures.Model.InventorySystem;
using Remagures.Tools;

namespace Remagures.Model.UpgradeSystem
{
    public readonly struct UpgradeItemData
    {
        public readonly IItem Item;
        public readonly int Cost;

        public UpgradeItemData(IItem item, int cost)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
            Cost = cost.ThrowExceptionIfLessOrEqualsZero();
        }
    }
}