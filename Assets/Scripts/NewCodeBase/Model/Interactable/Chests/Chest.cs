using System;
using Remagures.Inventory;
using Remagures.SO;
using Remagures.Tools.SwampAttack.Runtime.Tools.SaveSystem;

namespace Remagures.Model.Interactable
{
    public class Chest : IChest
    {
        public bool IsOpened { get; private set; }
        public BaseInventoryItem Item { get; }
        
        private readonly PlayerInventory _inventory;
        private readonly string _name;

        private readonly BinaryStorage _storage = new();

        public Chest(PlayerInventory inventory, BaseInventoryItem item, string name)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            Item = item ?? throw new ArgumentNullException(nameof(item));
            _name = name ?? throw new ArgumentNullException(nameof(name));
            
            if (_storage.Exist(_name))
                IsOpened = _storage.Load<bool>(_name);
        }

        public void Interact()
        {
            if (IsOpened)
                return;

            var newCell = new Cell(Item);
            var inventoryCell = _inventory.GetCell(newCell.Item);

            if (inventoryCell == null || (inventoryCell.CanAddItemAmount() && inventoryCell.CanMergeWithItem(newCell.Item)))
                _inventory.Add(newCell);

            IsOpened = true;
            _storage.Save(IsOpened, _name);
        }
    }
}
