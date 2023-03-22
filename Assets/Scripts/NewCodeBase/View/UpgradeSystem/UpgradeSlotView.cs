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

        public void Display(ItemUpgradeData<IUpgradableItem<IItem>> itemUpgradeData)
        {
            _itemImage.sprite = itemUpgradeData.ItemWhichUpgrading.Sprite;
            _itemName.text = itemUpgradeData.ItemWhichUpgrading.Name; 
            _costText.text = itemUpgradeData.Cost.ToString();
        }
    }
}
