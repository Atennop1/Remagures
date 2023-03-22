using System;
using System.Collections.Generic;
using Remagures.Factories;
using Remagures.Model.InventorySystem;
using Remagures.Model.UpgradeSystem;
using Remagures.Model.Wallet;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.View.UpgradeSystem
{
    public class UpgradeMenuView : MonoBehaviour
    {
        [SerializeField] private GameObject _absenceItemsGameObject;
        [SerializeField] private Transform _content;
        [SerializeField] private GameObject _windowGameObject;

        [Space]
        [SerializeField] private IUpgradeSlotsViewFactory _upgradeSlotsViewFactory;
        
        private List<IInventory<IUpgradableItem<IItem>>> _inventories;
        private List<IUpgradesChain<IUpgradableItem<IItem>>> _chains;

        public void Construct(List<IInventory<IUpgradableItem<IItem>>> inventories, List<IUpgradesChain<IUpgradableItem<IItem>>> chains)
        {
            _inventories = inventories ?? throw new ArgumentNullException(nameof(inventories));
            _chains = chains ?? throw new ArgumentNullException(nameof(chains));
        }

        private void OnEnable()
            => UpdateContent();
        
        private void CreateSlots() 
        {
            _absenceItemsGameObject.SetActive(true);

            foreach (var inventory in _inventories) 
            {
                foreach (var cell in inventory.Cells)
                {
                    foreach (var chain in _chains)
                    {
                        if (!chain.CanUpgradeItem(cell.Item)) 
                            continue;

                        var upgradeData = chain.GetUpgradeForItem(cell.Item);
                        var upgradeSlotView = _upgradeSlotsViewFactory.Create(_content);
                        
                        upgradeSlotView.UpgradeButton.onClick.AddListener(() =>
                        {
                            cell.Item.Upgrade(upgradeData);
                            UpdateContent();
                        });
                        
                        upgradeSlotView.Display(upgradeData);
                        _absenceItemsGameObject.SetActive(false);
                    }
                }
            }
        }

        private void UpdateContent()
        {
            ClearSlots();
            CreateSlots();
        }

        private void ClearSlots()
        {
            for (var i = 0; i < transform.childCount; i++)
                Destroy(transform.GetChild(i).gameObject);
        }

        private void Close()
        {
            Time.timeScale = 1;
            _windowGameObject.SetActive(false);
        }
    }
}
