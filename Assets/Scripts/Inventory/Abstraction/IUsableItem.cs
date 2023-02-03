using UnityEngine.Events;

namespace Remagures.Inventory
{
    public interface IUsableItem : IBaseItemComponent
    {
        public UnityEvent UsingEvent { get; }
        public void Use();
    }
}
