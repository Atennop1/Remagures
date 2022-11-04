using Remagures.Inventory.Abstraction;
using UnityEngine;
using UnityEngine.Events;

namespace Remagures.SO.Inventory.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/UsableItem")]
    public class UsableInventoryItem : BaseInventoryItem, IUsableItem
    {
        [field: SerializeField, Header("Usable Info")] public UnityEvent UsingEvent { get; private set; }

        public void Use()
        {
            UsingEvent.Invoke();
        }
    }
}
