using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/PlayerInventory")]
public class PlayerInventory : ScriptableObject
{ 
    [SerializeField] private List<BaseInventoryItem> _inventory = new List<BaseInventoryItem>();
    public IReadOnlyList<BaseInventoryItem> MyInventory => _inventory;

    public void Add(BaseInventoryItem item, bool increase)
    {
        if (item.ItemData.Stackable && increase)
            item.ItemData.NumberHeld++;

        if (!_inventory.Contains(item))
            _inventory.Add(item);
    }

    public void Remove(BaseInventoryItem item)
    {
        if (_inventory.Contains(item))
            _inventory.Remove(item);
    }

    public bool Contains(BaseInventoryItem item)
    {
        return _inventory.Contains(item);
    }

    public void Clear()
    {
        _inventory.Clear();
    }
}
