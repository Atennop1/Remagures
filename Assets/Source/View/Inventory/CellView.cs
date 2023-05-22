using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.Inventory
{
    public sealed class CellView : SerializedMonoBehaviour, ICellView
    {
        [field: SerializeField] public Button Button { get; private set; }
        [SerializeField] private Text _itemCount;
        
        [Space]
        [SerializeField] private Image _itemImage;
        [SerializeField] private Sprite _noneSprite;

        public void Display(IReadOnlyCell<IItem> cell)
        {
            if (cell == null) 
                return;
        
            _itemImage.sprite = cell.Item.Description != "" && cell.Item.Name != "" ? cell.Item.Sprite : _noneSprite;
            _itemCount.text = cell.ItemsCount > 1 ? cell.ItemsCount.ToString() : "";
        }
    }
}
