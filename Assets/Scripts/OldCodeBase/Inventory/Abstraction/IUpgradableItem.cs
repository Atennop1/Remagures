using System.Collections.Generic;
using Remagures.SO;

namespace Remagures.Inventory
{
    public interface IUpgradableItem : IBaseItemComponent
    {
        public int ThisItemLevel { get; }
        public int CostForThisItem { get; }
        public List<BaseInventoryItem> ItemsForLevels { get; }
    }
}
