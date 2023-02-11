using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public interface IDisplayableItem : IItem
    {
        AnimatorOverrideController AnimatorController { get; }
    }
}
