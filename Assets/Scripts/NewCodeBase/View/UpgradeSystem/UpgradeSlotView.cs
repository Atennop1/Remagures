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

        public void Display(UpgradeItemData upgradeItemData)
        {
            _itemImage.sprite = upgradeItemData.Item.Sprite;
            _itemName.text = upgradeItemData.Item.Name; 
            _costText.text = upgradeItemData.Cost.ToString();
        }
    }
}