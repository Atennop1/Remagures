using UnityEngine;

namespace Remagures.Inventory
{
    public interface IDisplayableItem : IBaseItemComponent
    {
        public AnimatorOverrideController OverrideController { get; }
    }
}
