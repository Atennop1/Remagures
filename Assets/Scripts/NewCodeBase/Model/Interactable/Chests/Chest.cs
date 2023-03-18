using System;
using System.Linq;
using Remagures.Model.InventorySystem;
using Remagures.Tools;

namespace Remagures.Model.Interactable
{
    public sealed class Chest : IChest
    {
        public bool IsOpened { get; private set; }
        public bool HasInteractionEnded { get; private set; }
        public IItem Item { get; }
        
        private readonly IInventory<IItem> _inventory;
        private readonly string _name;

        private readonly BinaryStorage _storage = new();

        public Chest(Inventory<IItem> inventory, IItem item, string name)
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

            var newCell = new Cell<IItem>(Item);
            var inventoryCell = _inventory.Cells.ToList().Find(cell => cell.Item == newCell.Item);

            if (inventoryCell == null || inventoryCell.CanAddItem(Item))
                _inventory.Add(newCell);

            IsOpened = true;
            HasInteractionEnded = true;
            _storage.Save(IsOpened, _name);
        }

        public void EndInteracting() { }
    }
}
