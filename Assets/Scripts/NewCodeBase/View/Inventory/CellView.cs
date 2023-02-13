using Remagures.Model.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.Inventory
{
    public sealed class CellView : MonoBehaviour, ICellView
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

        //public void Choice() //TODO move it to inventory view
        //{
        //    _view.UseButton.SetActive(ThisCell.Item.IsStackable || ThisCell.Item is IMagicItem);
        //    _view.SetText(ThisCell);
        //}
    }
}
