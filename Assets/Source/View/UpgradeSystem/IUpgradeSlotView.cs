using Remagures.Model.InventorySystem;
using Remagures.Model.UpgradeSystem;
using UnityEngine.UI;

namespace Remagures.View.UpgradeSystem
{
    public interface IUpgradeSlotView
    {
        Button UpgradeButton { get; }
        void Display(IUpgrade<IItem> upgrade);
    }
}