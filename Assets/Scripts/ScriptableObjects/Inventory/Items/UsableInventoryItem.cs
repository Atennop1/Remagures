using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/UsableItem")]
public class UsableInventoryItem : BaseInventoryItem
{
    [field: SerializeField] public UsableItemData UsableItemData { get; private set; } = new UsableItemData();

    public void Use()
    {
        UsableItemData.ThisEvent.Invoke();
        DecreaseAmount();
    }
}
