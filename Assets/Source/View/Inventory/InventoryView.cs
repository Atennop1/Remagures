using System;
using System.Collections.Generic;
using Remagures.Factories;
using Remagures.Model.InventorySystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.Inventory
{
    public sealed class InventoryView<T> : SerializedMonoBehaviour where T: IItem
    {
        [SerializeField] private ICellViewFactory _cellViewsFactory;
        [SerializeField] private ItemInfoView<T> _itemInfoView;

        [Space]
        [SerializeField] private Transform _cellsContent;
        [SerializeField] private Text _noItemsText;
        [SerializeField] private Button _useButton;
        
        private IInventory<T> _inventory;
        private List<ICellView> _createdCellViews;

        public void Construct(IInventory<T> inventory)
            => _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        
        private void OnEnable()
            => CreateSlots();
        
        private void OnDestroy()
            => ClearContent();

        private void Awake()
            => _useButton.onClick.AddListener(CreateSlots);

        private void CreateSlots()
        {
            if (_noItemsText != null)
                _noItemsText.gameObject.SetActive(_inventory.Cells.Count == 0);

            ClearContent();
            foreach (var cell in _inventory.Cells)
            {
                var cellView = _cellViewsFactory.Create();
                cellView.Display(cell as IReadOnlyCell<IItem>);
                
                cellView.Button.onClick.AddListener(() => _itemInfoView.Display(cell.Item));
                _createdCellViews.Add(cellView);
            }
        }

        private void ClearContent()
        {
            _createdCellViews.ForEach(cellView => cellView.Button.onClick.RemoveAllListeners());
            _createdCellViews.Clear();
            
            for (var i = 0; i < _cellsContent.transform.childCount; i++)
                Destroy(_cellsContent.transform.GetChild(i).gameObject);
        }

        private void CloseInventory()
            => gameObject.SetActive(false);
    }
}
