using Remagures.Inventory;
using Remagures.Inventory.Abstraction;
using Remagures.Inventory.View;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.RuneSystem
{
    public class RuneSlot : Slot
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
                ItemImage.sprite = ThisItem.ItemSprite;
                GetComponent<Button>().enabled = true;
            }
        }
    
        public void Select()
        {
            _runeView.Select(ThisItem as IRuneItem);
        }
    }
}
