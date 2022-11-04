using UnityEngine.Events;

namespace Remagures.Inventory.Abstraction
{
    public interface IUsableItem : IBaseItemComponent
    {
        public UnityEvent UsingEvent { get; }
        public void Use();
    }
}
