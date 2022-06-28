using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/ArmorItem")]
public class ArmorInventoryItem : DisplayableInventoryItem
{
    [field: SerializeField] public ArmorItemData ArmorItemData { get; private set; } = new ArmorItemData();
}
