using Remagures.Model.Buttons;
using Remagures.Model.InventorySystem;
using Remagures.Root;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.Inventory
{
    public class ItemInfoView<T> : MonoBehaviour, IItemInfoView<T> where T: IItem
    {
        [SerializeField] private IItemFactory<IItem> _nullItemFactory;
        [SerializeField] private IItemButton<T> _itemButton;
        
        [Space]
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _descriptionText;

        private void OnEnable()
            => Display((T)_nullItemFactory.Create());

        public void Display(T item)
        {
            _nameText.text = item.Name;
            _descriptionText.text = item.Description;
            _itemButton?.SetItem(item);
        }
    }
}