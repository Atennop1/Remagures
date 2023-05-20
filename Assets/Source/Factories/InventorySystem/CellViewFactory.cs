using System;
using Remagures.View.Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Factories
{
    public sealed class CellViewFactory : SerializedMonoBehaviour, ICellViewFactory
    {
        [SerializeField] private GameObject _inventorySlotPrefab;
        [SerializeField] private GameObject _inventoryPanel;
        
        public ICellView Create()
        {
            var slotObject = Instantiate(_inventorySlotPrefab, _inventoryPanel.transform.position, Quaternion.identity, _inventoryPanel.transform);
            return slotObject.GetComponent<ICellView>();
        }

        private void Awake()
        {
            if (!_inventorySlotPrefab.TryGetComponent(out ICellView _))
                throw new ArgumentNullException(nameof(_inventorySlotPrefab));
        }
    }
}