using System;
using System.Linq;
using Remagures.Root;
using Remagures.View.Inventory;

namespace Remagures.Model.InventorySystem
{
    public sealed class RunesSelector : IInventoryCellSelector<IRuneItem>, ILateUpdatable
    {
        public bool HasSelected { get; private set; }
        public IReadOnlyCell<IRuneItem> SelectedCell { get; private set;}

        private readonly IInventory<IRuneItem> _runesInventory;
        private readonly IItemInfoView<IRuneItem> _selectedRuneView;

        public RunesSelector(IInventory<IRuneItem> runesInventory, IItemInfoView<IRuneItem> selectedRuneView)
        {
            _runesInventory = runesInventory ?? throw new ArgumentNullException(nameof(runesInventory));
            _selectedRuneView = selectedRuneView ?? throw new ArgumentNullException(nameof(selectedRuneView));
        }

        public void LateUpdate()
            => HasSelected = false;
        
        public void Select(IRuneItem item)
        {
            if (_runesInventory.Cells.All(runeCell => runeCell.Item.Rune != item.Rune))
                throw new ArgumentException("This rune doesn't exist");
            
            foreach (var runeCell in _runesInventory.Cells)
                runeCell.Item.Rune.Deactivate();
            
            HasSelected = true;
            item.Rune.Activate();
            
            _selectedRuneView.Display(item);
            SelectedCell = _runesInventory.Cells.ToList().Find(cell => cell.Item.Rune == item.Rune);
        }

        public void UnSelect()
        {
            SelectedCell = null;
            foreach (var runeCell in _runesInventory.Cells)
                runeCell.Item.Rune.Deactivate();
        }
    }
}