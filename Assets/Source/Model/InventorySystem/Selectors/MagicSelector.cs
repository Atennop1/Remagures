using System;
using System.Linq;
using Remagures.Root;

namespace Remagures.Model.InventorySystem
{
    public sealed class MagicSelector : IInventoryCellSelector<IMagicItem>, ILateUpdatable
    {
        public bool HasSelected { get; private set; }
        public IReadOnlyCell<IMagicItem> SelectedCell { get; private set;}

        private readonly IInventory<IMagicItem> _magicInventory;

        public MagicSelector(IInventory<IMagicItem> runesInventory) 
            => _magicInventory = runesInventory ?? throw new ArgumentNullException(nameof(runesInventory));

        public void LateUpdate()
            => HasSelected = false;
        
        public void Select(IMagicItem item)
        {
            if (_magicInventory.Cells.All(runeCell => runeCell.Item.Magic != item.Magic))
                throw new ArgumentException("This magic doesn't exist");

            HasSelected = true;
            SelectedCell = _magicInventory.Cells.ToList().Find(cell => cell.Item.Magic == item.Magic);
        }

        public void UnSelect() 
            => SelectedCell = null;
    }
}