using UnityEngine;

namespace Remagures.Inventory.Abstraction
{
    public interface IDisplayableItem : IBaseItemComponent
    {
        public AnimatorOverrideController OverrideController { get; }
    }
}
