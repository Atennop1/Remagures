using System;
using Remagures.Factories;
using Remagures.Model.InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.Inventory
{
    public class InventoryView<T> : MonoBehaviour where T: IItem
    {
        [SerializeField] private IFactory<ICellView> _cellViewsFactory;
        [SerializeField] private ItemInfoView<T> _itemInfoView;

        [Space]
        [SerializeField] private Transform _cellsContent;
        [SerializeField] private Text _noItemsText;
        [SerializeField] private Button _useButton;
        
        private IInventory<T> _inventory;

        public void Construct(IInventory<T> inventory)
            => _inventory = inventory ?? throw new ArgumentNullException(nameof(inventory));
        
        private void OnEnable()
            => SetupCells();

        private void Awake()
            => _useButton.onClick.AddListener(SetupCells);

        private void CreateSlots()
        {
            if (_noItemsText != null)
                _noItemsText.gameObject.SetActive(_inventory.Cells.Count == 0);

            foreach (var cell in _inventory.Cells)
            {
                var cellView = _cellViewsFactory.Create();
                cellView.Display(cell as IReadOnlyCell<IItem>);
                cellView.Button.onClick.AddListener(() => _itemInfoView.Display(cell.Item));
            }
        }

        private void SetupCells()
        {
            DeleteAllCells();
            CreateSlots();
        }

        private void DeleteAllCells()
        {
            for (var i = 0; i < _cellsContent.transform.childCount; i++)
                Destroy(_cellsContent.transform.GetChild(i).gameObject);
        }

        private void CloseInventory()
            => gameObject.SetActive(false);
    }
}
