using Remagures.Player;
using UnityEngine;

namespace Remagures.Inventory
{
    public class MagicInventoryView : InventoryView
    {
        [Header("Magic Stuff")]
        [SerializeField] private MagicCounter _magicCounter;
        [SerializeField] private Slot _magicSlot;

        public void Equip()
        {
            _magicSlot.Setup(CurrentCell, this);

            if (CurrentCell.Item is IChoiceableItem choiceableItem)
                choiceableItem.SelectIn(PlayerInventory.MyInventory);

            _magicCounter.SetupProjectile((CurrentCell.Item as IMagicItem)?.Projectile);
        }

        protected override void SetButton() { }

        protected override void SetupPlayer() { }
    }
}
