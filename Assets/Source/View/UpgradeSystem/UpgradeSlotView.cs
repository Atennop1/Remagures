using Remagures.Model.InventorySystem;
using Remagures.Model.UpgradeSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.UpgradeSystem
{
    public sealed class UpgradeSlotView : SerializedMonoBehaviour, IUpgradeSlotView
    {
        [field: SerializeField] public Button UpgradeButton { get; private set; }
        
        [Space]
        [SerializeField] private Image _itemImage;
        [SerializeField] private Text _itemName;
        [SerializeField] private Text _costText;

        public void Display(IUpgrade<IItem> upgrade)
        {
            _itemImage.sprite = upgrade.Item.Sprite;
            _itemName.text = upgrade.Item.Name; 
            _costText.text = upgrade.BuyingData.Cost.ToString();
        }
    }
}
