using System.Collections.Generic;
using UnityEngine;

public class SaveInventorys : SaveLoad, ISaver
{
    [SerializeField] private GameSaveContainer _manager;
    [SerializeField] private List<ScriptableObject> _inventorys;
    [SerializeField] private List<ScriptableObject> _items;

    public void Save()
    {
        for (int j = 0; j < _inventorys.Count; j++)
        {
            _manager.CheckDir(Path + "/Inventory" + (j + 1));
            FloatValue value = ScriptableObject.CreateInstance<FloatValue>();
            PlayerInventory currentInventory = (_inventorys[j] as PlayerInventory);

            value.Value = currentInventory.MyInventory.Count;
            SaveObjectToJson(Path + "/Inventory" + (j + 1) + "/Count.json", value);

            for (int k = 0; k < currentInventory.MyInventory.Count; k++)
            {
                _manager.CheckDir(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1));
                FloatValue stackable = ScriptableObject.CreateInstance<FloatValue>();
                stackable.Value = currentInventory.MyInventory[k].ItemData.Stackable ? 1 : 0;
                SaveObjectToJson(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1) + "/Stackable.json", stackable);

                FloatValue id = ScriptableObject.CreateInstance<FloatValue>();
                if (currentInventory.MyInventory[k].ItemData.Stackable)
                    id.Value = (_manager.Savables[1] as SaveStackableItems).IndexOf(currentInventory.MyInventory[k]);
                else
                    id.Value = _items.IndexOf(currentInventory.MyInventory[k]);
                SaveObjectToJson(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1) + "/ID.json", id);

                if ((currentInventory.MyInventory[k] as MagicInventoryItem) != null || (currentInventory.MyInventory[k] as RuneInventoryItem) != null)
                {
                    BoolValue isCurrent = ScriptableObject.CreateInstance<BoolValue>();

                    if ((currentInventory.MyInventory[k] as MagicInventoryItem) != null)
                        isCurrent.Value = (currentInventory.MyInventory[k] as MagicInventoryItem).MagicItemData.IsCurrent;
                    else
                        isCurrent.Value = (currentInventory.MyInventory[k] as RuneInventoryItem).RuneItemData.IsCurrent;

                    SaveObjectToJson(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1) + "/IsCurrent.json", isCurrent);
                }
            }
        }
    }

    public void Load()
    {
        for (int j = 0; j < _inventorys.Count; j++)
        {
            FloatValue value = ScriptableObject.CreateInstance<FloatValue>();
            PlayerInventory currentInventory = (_inventorys[j] as PlayerInventory);
            currentInventory.Clear();

            _manager.CheckDir(Path + "/Inventory" + (j + 1));
            LoadObjectFromJson(Path + "/Inventory" + (j + 1) + "/Count.json", value);

            for (int k = 0; k < (int)value.Value; k++)
            {
                FloatValue id = ScriptableObject.CreateInstance<FloatValue>();
                FloatValue stackable = ScriptableObject.CreateInstance<FloatValue>();

                _manager.CheckDir(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1));
                LoadObjectFromJson(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1) + "/Stackable.json", stackable);
                LoadObjectFromJson(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1) + "/ID.json", id);

                if (stackable.Value == 1)
                    currentInventory.Add(((_manager.Savables[1] as SaveStackableItems).StackableItems[(int)id.Value] as BaseInventoryItem), false);
                else
                    currentInventory.Add((_items[(int)id.Value] as BaseInventoryItem), false);

                if ((currentInventory.MyInventory[k] as MagicInventoryItem) != null || (currentInventory.MyInventory[k] as RuneInventoryItem) != null)
                {
                    BoolValue isCurrent = ScriptableObject.CreateInstance<BoolValue>();
                    LoadObjectFromJson(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1) + "/IsCurrent.json", isCurrent);

                    if ((currentInventory.MyInventory[k] as MagicInventoryItem) != null)
                    {
                        if (isCurrent.Value == true)
                            (currentInventory.MyInventory[k] as MagicInventoryItem).MagicItemData.SetIsCurrent(currentInventory.MyInventory);
                        else
                            (currentInventory.MyInventory[k] as MagicInventoryItem).MagicItemData.DisableIsCurrent();
                    }
                    else
                    {
                        if (isCurrent.Value == true)
                            (currentInventory.MyInventory[k] as RuneInventoryItem).RuneItemData.SetIsCurrent(currentInventory.MyInventory);
                        else
                            (currentInventory.MyInventory[k] as RuneInventoryItem).RuneItemData.DisableIsCurrent();
                    }
                }
            }
        }
    }
    
    public void NewGame()
    {
        foreach (ScriptableObject inventory in _inventorys)
            (inventory as PlayerInventory).Clear();
        (_inventorys[2] as PlayerInventory).Add(_items[6] as BaseInventoryItem, false);

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i] as MagicInventoryItem != null)
                (_items[i] as MagicInventoryItem).MagicItemData.DisableIsCurrent();

            if (_items[i] as RuneInventoryItem != null)
            {
                (_items[i] as RuneInventoryItem).RuneItemData.ClassStat.ClearRunes();
                (_items[i] as RuneInventoryItem).RuneItemData.DisableIsCurrent();
            }
        }
    }
}
