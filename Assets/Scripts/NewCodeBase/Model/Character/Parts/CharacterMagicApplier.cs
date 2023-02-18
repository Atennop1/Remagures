using System;
using Remagures.Model.InventorySystem;

namespace Remagures.Model.Character
{
    public class CharacterMagicApplier
    {
        private readonly IInventoryCellSelector<IMagicItem> _magicCellsSelector;

        public CharacterMagicApplier(IInventoryCellSelector<IMagicItem> magicCellsSelector)
            => _magicCellsSelector = magicCellsSelector ?? throw new ArgumentNullException(nameof(magicCellsSelector));

        public void Apply()
            => _magicCellsSelector.SelectedCell.Item.Use();
    }
}