using Remagures.View.UpgradeSystem;
using UnityEngine;

namespace Remagures.Factories
{
    public interface IUpgradeSlotsViewFactory
    {
        IUpgradeSlotView Create(Transform parent);
    }
}