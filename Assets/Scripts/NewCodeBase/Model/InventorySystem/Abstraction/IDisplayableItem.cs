using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public interface IDisplayableItem : IBaseItemComponent
    {
        public AnimatorOverrideController OverrideController { get; }
    }
}
