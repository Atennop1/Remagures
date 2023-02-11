using UnityEngine.Events;

namespace Remagures.Model.InventorySystem
{
    public interface IUsableItem : IBaseItemComponent
    {
        public UnityEvent UsingEvent { get; }
        public void Use();
    }
}
