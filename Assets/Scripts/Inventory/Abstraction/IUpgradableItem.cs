using System.Collections.Generic;
using Remagures.SO.Inventory.Items;

namespace Remagures.Inventory.Abstraction
{
    public interface IUpgradableItem : IBaseItemComponent
    {
        public int ThisItemLevel { get; }
        public int CostForThisItem { get; }
        public List<BaseInventoryItem> ItemsForLevels { get; }
    }
}
