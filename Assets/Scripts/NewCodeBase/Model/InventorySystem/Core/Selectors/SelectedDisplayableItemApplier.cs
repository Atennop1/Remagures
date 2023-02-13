using System;
using Remagures.Root;
using Remagures.View.Inventory;

namespace Remagures.Model.InventorySystem
{
    public class SelectedDisplayableItemApplier<T> : IUpdatable where T: IDisplayableItem
    {
        private readonly IInventoryItemSelector<T> _selector;
        private readonly IDisplayableItemView _displayableItemView;

        public SelectedDisplayableItemApplier(IInventoryItemSelector<T> selector, IDisplayableItemView displayableItemView)
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