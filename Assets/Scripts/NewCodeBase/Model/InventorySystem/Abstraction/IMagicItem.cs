using Remagures.Components;
using UnityEngine.Events;

namespace Remagures.Model.InventorySystem
{
    public interface IMagicItem : IBaseItemComponent
    {
        public Projectile Projectile { get; }
        public UnityEvent UsingEvent { get; }
    }
}
