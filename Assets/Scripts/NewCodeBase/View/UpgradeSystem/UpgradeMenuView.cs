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
        [SerializeField] private Text _sharpsCountText;
        [SerializeField] private GameObject _absenceItemsGameObject;

        [Space] 
        [SerializeField] private Transform _content;
        [SerializeField] private GameObject _windowGameObject;

        [Space]
        [SerializeField] private IUpgradeSlotsViewFactory _upgradeSlotsViewFactory;
        
        private List<IInventory<IItem>> _inventories;
        private List<IUpgrader> _upgraders;
        private IWallet _sharpsWallet;

        public void Construct(List<IInventory<IItem>> inventories, List<IUpgrader> upgraders, IWallet sharpsWallet)
        {
            _inventories = inventories ?? throw new ArgumentNullException(nameof(inventories));
            _upgraders = upgraders ?? throw new ArgumentNullException(nameof(upgraders));
            _sharpsWallet = sharpsWallet ?? throw new ArgumentNullException(nameof(sharpsWallet));
        }

        private void OnEnable()
        {
            UpdateContent();
            _sharpsCountText.text = _sharpsWallet.Money.ToString();
        }

        private void CreateSlots() 
        {
            _absenceItemsGameObject.SetActive(true);

            foreach (var inventory in _inventories) //TODO throw this to another component
            {
                foreach (var cell in inventory.Cells)
                {
                    foreach (var upgrader in _upgraders)
                    {
                        if (!upgrader.CanUpgradeItem(cell.Item)) 
                            continue;

                        var upgradeData = upgrader.GetUpgradedItemData(cell.Item);
                        var upgradeSlot = _upgradeSlotsViewFactory.Create(_content);
                        
                        upgradeSlot.UpgradeButton.onClick.AddListener(() =>
                        {
                            upgrader.Upgrade(cell.Item);
                            UpdateContent();
                            _sharpsCountText.text = _sharpsWallet.Money.ToString();
                        });
                        
                        upgradeSlot.Display(upgradeData);
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
