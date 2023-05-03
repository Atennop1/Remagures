using System;
using Remagures.Model.InventorySystem;
using Remagures.View.Inventory;

namespace Remagures.Model.UI
{
    public sealed class SelectRuneButton : IItemButton<IRuneItem>
    {
        private readonly IInventoryCellSelector<IRuneItem> _cellSelector;
        private readonly ICellView _cellView;
        private IRuneItem _item;

        public SelectRuneButton(IInventoryCellSelector<IRuneItem> cellSelector, ICellView cellView)
        {
            _cellSelector = cellSelector ?? throw new ArgumentNullException(nameof(cellSelector));
            _cellView = cellView ?? throw new ArgumentNullException(nameof(cellView));
        }

        public void SetItem(IRuneItem item) 
            => _item = item ?? throw new ArgumentNullException(nameof(item));

        public void Press()
        {
            _cellSelector.Select(_item);
            _cellView.Display(new Cell<IItem>(_item));
        }
    }
}