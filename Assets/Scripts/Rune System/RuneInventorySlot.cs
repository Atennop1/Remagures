using UnityEngine;
using UnityEngine.UI;

public class RuneInventorySlot : InventorySlot
{
    [SerializeField] private RuneView _runeView;

    public void OnEnable()
    {
        if (!_runeView.Inventory.Contains(new Cell(ThisItem, 1)))
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
