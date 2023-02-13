using Remagures.Model.InventorySystem;
using UnityEngine;

namespace Remagures.View.Inventory
{
    public class MagicInventoryView : MonoBehaviour //TODO rework this after making magic system
    {
        [Header("Magic Stuff")]
        [SerializeField] private MagicCounter _magicCounter;
        [SerializeField] private CellView magicCellView;

        public void Equip()
        {
            magicCellView.Display(CurrentCell, this);

            if (CurrentCell.Item is IChoiceableItem choiceableItem)
                choiceableItem.SelectIn(Inventory.Cells);

            _magicCounter.SetupProjectile((CurrentCell.Item as IMagicItem)?.Projectile);
        }
    }
}
