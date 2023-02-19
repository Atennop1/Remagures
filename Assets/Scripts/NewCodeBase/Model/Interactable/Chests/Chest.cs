using System;
using System.Linq;
using Remagures.Model.InventorySystem;
using Remagures.SO;
using Remagures.Tools;
using Remagures.Tools.SwampAttack.Runtime.Tools.SaveSystem;

namespace Remagures.Model.Interactable
{
    public sealed class Chest : IChest
    {
        public bool IsOpened { get; private set; }
        public bool HasInteracted { get; private set; }
        public Item Item { get; }
        
        private readonly IInventory _inventory;
        private readonly string _name;

        private readonly BinaryStorage _storage = new();

        public Chest(Inventory inventory, Item item, string name)
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
            var inventoryCell = _inventory.Cells.ToList().Find(cell => cell.Item == newCell.Item);

            if (inventoryCell == null || inventoryCell.CanAddItem(Item))
                _inventory.Add(newCell);

            IsOpened = true;
            HasInteracted = true;
            _storage.Save(IsOpened, _name);
        }

        public void EndInteracting() { }
    }
}
