using Remagures.Model.InventorySystem;

namespace Remagures.Model.Character
{
    public class CharacterMagicApplier
    {
        private readonly IInventoryCellSelector<IMagicItem> _magicCellsSelector;

        public void Apply()
            => _magicCellsSelector.SelectedCell.Item.Use();
    }
}