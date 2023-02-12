using Remagures.Model.InventorySystem;
using Remagures.View.Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.RuneSystem
{
    public class RuneCellView : CellView
    {
        [SerializeField] private RuneView _runeView;

        public void OnEnable()
        {
            if (!_runeView.Inventory.Contains(new Cell(ThisItem)))
            {
                ItemImage.sprite = NoneSprite;
                GetComponent<Button>().enabled = false;
            }
            else
            {
                ItemImage.sprite = ThisItem.Sprite;
                GetComponent<Button>().enabled = true;
            }
        }

        public void Select()
            => _runeView.Select(ThisItem as IRuneItem);
    }
}
