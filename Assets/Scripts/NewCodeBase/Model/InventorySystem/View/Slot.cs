using Remagures.SO;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.Model.InventorySystem
{
    public class Slot : MonoBehaviour
    {
        [field: SerializeField, Header("UI")] protected Image ItemImage { get; private set; }
        [field: SerializeField] protected Sprite NoneSprite { get; private set; }
        [field: SerializeField] protected Text ItemCount { get; private set; }

        [field: SerializeField, Space] public Item ThisItem { get; private set; }
        public IReadOnlyCell ThisCell { get; private set; }
        private InventoryView _view;

        public void Setup(IReadOnlyCell newCell, InventoryView newView)
        {
            ThisCell = newCell;
            ThisItem = newCell.Item;
            _view = newView;

            if (ThisCell == null) 
                return;
        
            ItemImage.sprite = ThisCell.Item.Description != "" && ThisCell.Item.Name != "" ? ThisCell.Item.Sprite : NoneSprite;
            ItemCount.text = ThisCell.ItemsCount > 1 ? ThisCell.ItemsCount.ToString() : "";
        }

        public void Choice()
        {
            _view.UseButton.SetActive(ThisCell.Item.IsStackable || ThisCell.Item is IMagicItem);
            _view.SetText(ThisCell);
        }
    }
}
