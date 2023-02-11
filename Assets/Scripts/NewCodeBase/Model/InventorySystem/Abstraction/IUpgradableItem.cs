using System.Collections.Generic;
using Remagures.SO;

namespace Remagures.Model.InventorySystem
{
    public interface IUpgradableItem : IBaseItemComponent
    {
        public int ThisItemLevel { get; }
        public int CostForThisItem { get; }
        public List<BaseInventoryItem> ItemsForLevels { get; }
    }
}
