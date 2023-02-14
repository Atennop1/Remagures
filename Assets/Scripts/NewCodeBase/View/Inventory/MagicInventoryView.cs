using Remagures.Model;
using Remagures.Model.InventorySystem;
using UnityEngine;

namespace Remagures.View.Inventory
{
    public class MagicInventoryView : MonoBehaviour //TODO rework this after making magic system
    {
        [Header("Magic Stuff")]
        [SerializeField] private Mana mana;
        [SerializeField] private CellView magicCellView;

        public void Equip()
        {
            magicCellView.Display(CurrentCell, this);

            if (CurrentCell.Item is IChoiceableItem choiceableItem)
                choiceableItem.SelectIn(Inventory.Cells);

            mana.SetupProjectile((CurrentCell.Item as IMagicItem)?.Projectile);
        }
    }
}
