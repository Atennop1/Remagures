using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items/BaseItem")]
public class BaseInventoryItem : ScriptableObject
{
    [field: SerializeField] public BaseItemData ItemData { get; private set; } = new BaseItemData();

    public void DecreaseAmount()
    {
        ItemData.NumberHeld--;
        if (ItemData.NumberHeld < 0)
            ItemData.NumberHeld = 0;
    }
}
