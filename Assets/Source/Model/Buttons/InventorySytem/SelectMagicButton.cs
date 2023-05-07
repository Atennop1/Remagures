using System;
using Remagures.Model.InventorySystem;
using Remagures.View.Inventory;

namespace Remagures.Model.Buttons
{
    public sealed class SelectMagicButton : IItemButton<IMagicItem>
    {
        private readonly IInventoryCellSelector<IMagicItem> _cellSelector;
        private readonly ICellView _cellView;
        private IMagicItem _item;

        public SelectMagicButton(IInventoryCellSelector<IMagicItem> cellSelector, ICellView cellView)
        {
            _cellSelector = cellSelector ?? throw new ArgumentNullException(nameof(cellSelector));
            _cellView = cellView ?? throw new ArgumentNullException(nameof(cellView));
        }

        public void SetItem(IMagicItem item) 
            => _item = item ?? throw new ArgumentNullException(nameof(item));

        public void Press()
        {
            _cellSelector.Select(_item);
            _cellView.Display(new Cell<IItem>(_item));
        }
    }
}