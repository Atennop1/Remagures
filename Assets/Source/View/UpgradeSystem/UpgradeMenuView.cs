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
        private List<IUpgradeSlotView> _upgradeSlotsView;

        public void Construct(List<IUpgradesChain<TItem>> chains, IInventory<TItem> inventory)
        {
            _chains = chains ?? throw new ArgumentNullException(nameof(chains));
            _inventory = inventory ?? throw new ArgumentNullException(name);
        }

        private void OnDestroy()
            => ClearSlots();

        private void OnEnable()
            => CreateSlots();

        private void CreateSlots()
        {
            ClearSlots();
            _absenceItemsGameObject.SetActive(true);

            foreach (var chain in _chains)
            {
                foreach (var cell in _inventory.Cells)
                {
                    if (!chain.CanUpgrade(cell.Item))
                        continue;

                    var upgrade = chain.GetNextUpgrade(cell.Item);
                    var upgradeSlotView = _upgradeSlotsViewFactory.Create(_content);

                    upgradeSlotView.UpgradeButton.onClick.AddListener(() =>
                    {
                        chain.Upgrade(cell.Item);
                        CreateSlots();
                    });

                    _upgradeSlotsView.Add(upgradeSlotView);
                    upgradeSlotView.Display(upgrade as IUpgrade<IItem>);
                    _absenceItemsGameObject.SetActive(false);
                }
            }
        }

        private void ClearSlots()
        {
            _upgradeSlotsView.ForEach(slotView => slotView.UpgradeButton.onClick.RemoveAllListeners());
            _upgradeSlotsView.Clear();
            
            for (var i = 0; i < _content.childCount; i++)
                Destroy(transform.GetChild(i).gameObject);
        }

        private void Close()
        {
            Time.timeScale = 1;
            _windowGameObject.SetActive(false);
        }
    }
}
