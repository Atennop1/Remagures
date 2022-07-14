using System.Collections.Generic;
using UnityEngine;

public class SaveInventorys : Saver, ISaver
{
    [SerializeField] private GameSaveContainer _container;
    [SerializeField] private List<ScriptableObject> _inventorys;
    [SerializeField] private List<ScriptableObject> _items;

    public void Save()
    {
        for (int j = 0; j < _inventorys.Count; j++)
        {
            _container.CheckDir(Path + "/Inventory" + (j + 1));
            FloatValue value = ScriptableObject.CreateInstance<FloatValue>();
            PlayerInventory currentInventory = (_inventorys[j] as PlayerInventory);

            value.Value = currentInventory.MyInventory.Count;
            SaveObjectToJson(Path + "/Inventory" + (j + 1) + "/Count.json", value);

            for (int k = 0; k < currentInventory.MyInventory.Count; k++)
            {
                _container.CheckDir(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1));

                value.Value = currentInventory.MyInventory[k].ItemCount;
                SaveObjectToJson(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1) + "/Count.json", value);

                value.Value = _items.IndexOf(currentInventory.MyInventory[k].Item as ScriptableObject);
                SaveObjectToJson(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1) + "/ID.json", value);

                if (currentInventory.MyInventory[k].Item is IChoiceableItem )
                {
                    BoolValue isCurrent = ScriptableObject.CreateInstance<BoolValue>();
                    isCurrent.Value = (currentInventory.MyInventory[k].Item as IChoiceableItem).IsCurrent;
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

            _container.CheckDir(Path + "/Inventory" + (j + 1));
            LoadObjectFromJson(Path + "/Inventory" + (j + 1) + "/Count.json", value);

            for (int k = 0; k < (int)value.Value; k++)
            {
                FloatValue id = ScriptableObject.CreateInstance<FloatValue>();
                FloatValue count = ScriptableObject.CreateInstance<FloatValue>();

                _container.CheckDir(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1));

                LoadObjectFromJson(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1) + "/Count.json", count);
                LoadObjectFromJson(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1) + "/ID.json", id);

                currentInventory.Add(new Cell((_items[(int)id.Value] as BaseInventoryItem), (int)count.Value));

                if (currentInventory.MyInventory[k].Item is IChoiceableItem)
                {
                    BoolValue isCurrent = ScriptableObject.CreateInstance<BoolValue>();
                    LoadObjectFromJson(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1) + "/IsCurrent.json", isCurrent);
                    if (isCurrent.Value == true)
                        (currentInventory.MyInventory[k].Item as IChoiceableItem).SetIsCurrent(currentInventory.MyInventory);
                    else
                        (currentInventory.MyInventory[k].Item as IChoiceableItem).DisableIsCurrent();
                }
            }
        }
    }
    
    public void NewGame()
    {
        foreach (ScriptableObject inventory in _inventorys)
            (inventory as PlayerInventory).Clear();
        (_inventorys[2] as PlayerInventory).Add(new Cell(_items[6] as BaseInventoryItem, 1));

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i] is IChoiceableItem)
                (_items[i] as IChoiceableItem).DisableIsCurrent();

            if (_items[i] is IRuneItem)
                (_items[i] as IRuneItem).ClassStat.ClearRunes();
        }
    }
}
