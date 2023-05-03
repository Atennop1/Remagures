using Remagures.Model.InventorySystem;
using Remagures.Model.UI;
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

        private RectTransform _descriptionTextRect;

        private void Awake() 
            => _descriptionTextRect = _descriptionText.gameObject.GetComponent<RectTransform>();

        private void OnEnable()
            => Display((T)_nullItemFactory.Create());

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
            _itemButton.SetItem(item);

            var anchoredPositionX = isItemUsable ? -81.58096f : 6.96f;
            var sizeDeltaX = isItemUsable ? 1941.4f : 2806.157f;
            
            _descriptionTextRect.anchoredPosition = new Vector2(anchoredPositionX, _descriptionTextRect.anchoredPosition.y);
            _descriptionTextRect.sizeDelta = new Vector2(sizeDeltaX, _descriptionTextRect.sizeDelta.y);
        }
    }
}