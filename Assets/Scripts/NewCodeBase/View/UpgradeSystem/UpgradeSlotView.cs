using Remagures.Model.InventorySystem;
using Remagures.Model.UpgradeSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.UpgradeSystem
{
    public sealed class UpgradeSlotView : MonoBehaviour, IUpgradeSlotView
    {
        [field: SerializeField] public Button UpgradeButton { get; private set; }
        
        [Space]
        [SerializeField] private Image _itemImage;
        [SerializeField] private Text _itemName;
        [SerializeField] private Text _costText;

        public void Display(IUpgradeLevel<IItem> upgradeLevel)
        {
            _itemImage.sprite = upgradeLevel.UpgradedItem.Sprite;
            _itemName.text = upgradeLevel.UpgradedItem.Name; 
            _costText.text = upgradeLevel.BuyingData.Cost.ToString();
        }
    }
}
