using System;
using System.Linq;
using Remagures.Model.InventorySystem;
using Remagures.Tools;
using SaveSystem;

namespace Remagures.Model.Interactable
{
    public sealed class Chest : IChest
    {
        public bool IsOpened { get; private set; }
        public bool HasInteractionEnded { get; private set; }
        public IItem Item { get; }

        private readonly IInventory<IItem> _inventory;

        public Chest(IInventory<IItem> inventory, IItem item)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            Item = item ?? throw new ArgumentNullException(nameof(item));
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
        }

        public void EndInteracting() { }
    }
}
