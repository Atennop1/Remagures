using System;
using System.Collections.Generic;
using Remagures.Factories;
using Remagures.Model.InventorySystem;
using Remagures.Model.UpgradeSystem;
using UnityEngine;

namespace Remagures.View.UpgradeSystem
{
    public sealed class UpgradeMenuView<TItem> : MonoBehaviour where TItem: IItem
    {
        [SerializeField] private GameObject _absenceItemsGameObject;
        [SerializeField] private Transform _content;
        [SerializeField] private GameObject _windowGameObject;

        [Space]
        [SerializeField] private IUpgradeSlotsViewFactory _upgradeSlotsViewFactory;
        
        private List<IUpgradesChain<TItem>> _chains;
        private IInventory<TItem> _inventory;

        public void Construct(List<IUpgradesChain<TItem>> chains, IInventory<TItem> inventory)
        {
            _chains = chains ?? throw new ArgumentNullException(nameof(chains));
            _inventory = inventory ?? throw new ArgumentNullException(name);
        }

        private void OnEnable()
            => UpdateContent();

        private void CreateSlots()
        {
            _absenceItemsGameObject.SetActive(true);

            foreach (var chain in _chains)
            {
                foreach (var cell in _inventory.Cells)
                {
                    if (!chain.CanAdvance(cell.Item))
                        continue;

                    var currentLevel = chain.GetCurrentLevel(cell.Item);
                    var upgradeSlotView = _upgradeSlotsViewFactory.Create(_content);

                    upgradeSlotView.UpgradeButton.onClick.AddListener(() =>
                    {
                        chain.Advance(cell.Item);
                        UpdateContent();
                    });

                    upgradeSlotView.Display(currentLevel as IUpgradeLevel<IItem>);
                    _absenceItemsGameObject.SetActive(false);
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
