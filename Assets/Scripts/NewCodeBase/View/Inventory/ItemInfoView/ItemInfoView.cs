using System;
using System.Linq;
using Remagures.Model.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.Inventory
{
    public class ItemInfoView<T> : MonoBehaviour, IItemInfoView<T> where T: IItem
    {
        [SerializeField] private Button _useButton;
        [SerializeField] private Text _nameText;
        [SerializeField] private Text _descriptionText;

        private RectTransform _descriptionTextRect;
        private IInventory<T> _inventory;
        
        private readonly IItem _nullItem = new NullItem();
        private Action _useButtonListener;

        public void Construct(IInventory<T> inventory)
            => _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));

        private void Awake()
        {
            _descriptionTextRect = _descriptionText.gameObject.GetComponent<RectTransform>();
            _useButton.onClick.AddListener(() => _useButtonListener.Invoke());
        }

        private void OnEnable()
            => Display((T)_nullItem);

        public void Display(T item)
        {
            _nameText.text = item.Name;
            _descriptionText.text = item.Description;
            SetupButton(item);
        }

        private void SetupButton(T item)
        {
            var isItemUsable = item is IUsableItem;
            _useButton.gameObject.SetActive(isItemUsable);
            _useButtonListener = () => UseItem(item);

            var anchoredPositionX = isItemUsable ? -81.58096f : 6.96f;
            var sizeDeltaX = isItemUsable ? 1941.4f : 2806.157f;
            
            _descriptionTextRect.anchoredPosition = new Vector2(anchoredPositionX, _descriptionTextRect.anchoredPosition.y);
            _descriptionTextRect.sizeDelta = new Vector2(sizeDeltaX, _descriptionTextRect.sizeDelta.y);
        }
        
        private void UseItem(T item)
        {
            (item as IUsableItem)?.Use();
            _inventory.Decrease(new Cell<T>(item));

            if (_inventory.Cells.ToList().Find(cell => cell.Item.Equals(item)) != null)
                Display((T)_nullItem);
        }
    }
}