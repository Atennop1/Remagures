using System.Globalization;
using Remagures.Model.Character;
using Remagures.Model.InventorySystem;
using Remagures.SO;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.UpgradeSystem
{
    public class UpgradeSlot : MonoBehaviour
    {
        [SerializeField] private UniqueSetup _uniqueSetup;
        
        [Header("Objects")]
        [SerializeField] private Image _itemImage;
        [SerializeField] private Text _itemName;
        [SerializeField] private Text _costText;

        [Space]
        [SerializeField] private FloatValue _sharps;

        public IReadOnlyCell ThisCell { get; private set; }
        private UpgradeView _view;
        private Player _player;
        
        public void Setup(IReadOnlyCell cell, UpgradeView view, Player player)
        {
            if (cell.Item == null) return;
        
            _view = view;
            ThisCell = cell;
            _player = player;

            _itemImage.sprite = cell.Item.Sprite;
            _itemName.text = cell.Item.Name;

            if (ThisCell.Item is IUpgradableItem currentItem) _costText.text = currentItem.CostForThisItem.ToString();
        }

        public void Buy()
        {
            if (ThisCell.Item is IUpgradableItem currentItem)
            {
                _view.Inventory.Remove(new Cell(currentItem.ItemsForLevels[currentItem.ThisItemLevel - 2]));
                _view.Inventory.Add(new Cell(currentItem as Item));

                _uniqueSetup.SetupUnique(_player);
                _sharps.Value -= currentItem.CostForThisItem;
            }

            _view.SharpsCountText.text = _sharps.Value.ToString(CultureInfo.InvariantCulture); 
            _view.OnEnable();
        }
    }
}
