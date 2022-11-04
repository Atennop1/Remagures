using Remagures.Components.Projectiles;
using UnityEngine.Events;

namespace Remagures.Inventory.Abstraction
{
    public interface IMagicItem : IBaseItemComponent
    {
        public Projectile Projectile { get; }
        public UnityEvent UsingEvent { get; }
    }
}
