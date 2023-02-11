using System.Collections.Generic;
using Remagures.Model.InventorySystem;
using Remagures.SO;
using UnityEngine;

namespace Remagures.Tools
{
    public class SaveInventorys : Saver, ISaver
    {
        [SerializeField] private GameSaver _container;
        [SerializeField] private List<ScriptableObject> _inventorys;
        [SerializeField] private List<ScriptableObject> _items;

        public void Save()
        {
            for (int j = 0; j < _inventorys.Count; j++)
            {
                _container.CheckDir(Path + "/Inventory" + (j + 1));
                var value = ScriptableObject.CreateInstance<FloatValue>();
                var currentInventory = _inventorys[j] as Inventory;

                if (currentInventory == null) continue;
            
                value.Value = currentInventory.Cells.Count;
                SaveObjectToJson(Path + "/Inventory" + (j + 1) + "/Count.json", value);

                for (var k = 0; k < currentInventory.Cells.Count; k++)
                {
                    _container.CheckDir(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1));

                    value.Value = currentInventory.Cells[k].ItemsCount;
                    SaveObjectToJson(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1) + "/Count.json", value);

                    value.Value = _items.IndexOf(currentInventory.Cells[k].Item);
                    SaveObjectToJson(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1) + "/ID.json", value);

                    if (currentInventory.Cells[k].Item is not IChoiceableItem) continue;
                
                    var isCurrent = ScriptableObject.CreateInstance<BoolValue>();
                    isCurrent.Value = ((IChoiceableItem)currentInventory.Cells[k].Item).IsCurrent;
                
                    SaveObjectToJson(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1) + "/IsCurrent.json", isCurrent);
                }
            }
        }

        public void Load()
        {
            for (var j = 0; j < _inventorys.Count; j++)
            {
                var value = ScriptableObject.CreateInstance<FloatValue>();
                var currentInventory = _inventorys[j] as Inventory;

                if (currentInventory == null) continue;
                currentInventory.Clear();

                _container.CheckDir(Path + "/Inventory" + (j + 1));
                LoadObjectFromJson(Path + "/Inventory" + (j + 1) + "/Count.json", value);

                for (var k = 0; k < (int)value.Value; k++)
                {
                    var id = ScriptableObject.CreateInstance<FloatValue>();
                    var count = ScriptableObject.CreateInstance<FloatValue>();

                    _container.CheckDir(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1));

                    LoadObjectFromJson(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1) + "/Count.json", count);
                    LoadObjectFromJson(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1) + "/ID.json", id);

                    currentInventory.Add(new Cell((_items[(int)id.Value] as Item), (int)count.Value));

                    if (currentInventory.Cells[k].Item is not IChoiceableItem) continue;
                
                    var isCurrent = ScriptableObject.CreateInstance<BoolValue>();
                    LoadObjectFromJson(Path + "/Inventory" + (j + 1) + "/Item" + (k + 1) + "/IsCurrent.json", isCurrent);
                
                    if (isCurrent.Value)
                        (currentInventory.Cells[k].Item as IChoiceableItem)?.SelectIn(currentInventory.Cells);
                    else
                        (currentInventory.Cells[k].Item as IChoiceableItem)?.DisableIsCurrent();
                }
            }
        }
    
        public void NewGame()
        {
            foreach (var inventory in _inventorys)
                (inventory as Inventory)?.Clear();
        
            (_inventorys[2] as Inventory)?.Add(new Cell(_items[6] as Item));
            foreach (var item in _items)
            {
                if (item is IChoiceableItem choiceableItem)
                    choiceableItem.DisableIsCurrent();

                if (item is IRuneItem runeItem)
                    runeItem.CharacterInfo.ClearRunes();
            }
        }
    }
}
