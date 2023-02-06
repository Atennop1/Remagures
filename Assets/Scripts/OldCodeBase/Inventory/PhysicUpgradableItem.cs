using System.Linq;

namespace Remagures.Inventory
{
    public class PhysicUpgradableItem : PhysicItem
    {
        protected override bool CanAddItem()
        {
            foreach (var cell in PlayerInventory.MyInventory)
            {
                var items = (cell.Item as IUpgradableItem)?.ItemsForLevels;
                if (items == null) continue;
            
                if (items.Any(item => item == ThisItem || items.IndexOf(item) < items.IndexOf((ThisItem))))
                    return false;
            }

            return true;
        }
    }
}
