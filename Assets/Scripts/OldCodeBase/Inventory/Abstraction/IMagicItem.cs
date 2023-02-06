using Remagures.Components;
using UnityEngine.Events;

namespace Remagures.Inventory
{
    public interface IMagicItem : IBaseItemComponent
    {
        public Projectile Projectile { get; }
        public UnityEvent UsingEvent { get; }
    }
}
