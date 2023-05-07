using System;
using System.Linq;
using Remagures.Model.InventorySystem;
using Remagures.Root;
using Remagures.Tools;
using Remagures.View.Inventory;

namespace Remagures.Model.Buttons
{
    public sealed class UseItemButton : IItemButton<IUsableItem>
    {
        private readonly IInventory<IUsableItem> _inventory;
        private readonly IItemInfoView<IUsableItem> _itemInfoView;
        private readonly IItemFactory<IItem> _nullItemFactory;
        private IUsableItem _item;

        public UseItemButton(IInventory<IUsableItem> inventory, IItemInfoView<IUsableItem> itemInfoView, IItemFactory<IItem> nullItemFactory)
        {
            _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
            _itemInfoView = itemInfoView ?? throw new ArgumentNullException(nameof(itemInfoView));
            _nullItemFactory = nullItemFactory ?? throw new ArgumentNullException(nameof(nullItemFactory));
        }

        public void SetItem(IUsableItem item) 
            => _item = item ?? throw new ArgumentNullException(nameof(item));

        public void Press()
        {
            _item?.Use();
            _inventory.Remove(new Cell<IUsableItem>(_item));

            if (!_inventory.Cells.ToList().Any(cell => cell.Item.AreEquals(_item)))
                _itemInfoView.Display((IUsableItem)_nullItemFactory.Create());
        }
    }
}