using UnityEngine;
using UnityEngine.UI;

public class RuneInventorySlot : InventorySlot
{
    [SerializeField] private RuneView _runeView;

    public void OnEnable()
    {
        if (!_runeView.Inventory.Contains(ThisItem))
        {
            ItemImage.sprite = NoneSprite;
            GetComponent<Button>().enabled = false;
        }
        else
        {
            ItemImage.sprite = ThisItem.ItemData.ItemSprite;
            GetComponent<Button>().enabled = true;
        }
    }
    
    public void Select()
    {
        _runeView.Select(ThisItem as RuneInventoryItem);
    }
}
