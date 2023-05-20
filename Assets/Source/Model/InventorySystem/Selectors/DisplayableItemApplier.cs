using System;
using Remagures.Root;
using Remagures.View.Inventory;

namespace Remagures.Model.InventorySystem
{
    public sealed class DisplayableItemApplier<T> : IUpdatable where T: IDisplayableItem
    {
        private readonly IInventoryCellSelector<T> _selector;
        private readonly IDisplayableItemView _displayableItemView;

        public DisplayableItemApplier(IInventoryCellSelector<T> selector, IDisplayableItemView displayableItemView)
        {
            _selector = selector ?? throw new ArgumentNullException(nameof(selector));
            _displayableItemView = displayableItemView ?? throw new ArgumentNullException(nameof(displayableItemView));
        }

        public void Update()
        {
            if (_selector.HasSelected)
                _displayableItemView.Display(_selector.SelectedCell.Item);
        }
    }
}