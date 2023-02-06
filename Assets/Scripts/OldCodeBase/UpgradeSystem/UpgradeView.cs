using System.Globalization;
using Remagures.Inventory;
using Remagures.Model.Character;
using Remagures.SO;
using UnityEngine;
using UnityEngine.UI;

namespace Remagures.UpgradeSystem
{
    public class UpgradeView : MonoBehaviour
    {
        [SerializeField] private UniqueSetup _uniqueSetup;
        
        [Header("Values")]
        [SerializeField] private FloatValue _sharps;
        [field: SerializeField] public PlayerInventory Inventory { get; private set; }
        [SerializeField] private Player _player;

        [field: SerializeField, Header("Objects")] public Text SharpsCountText { get; private set; }
        [SerializeField] private GameObject _noItems;
        [SerializeField] private GameObject _slotPrefab;

        public void OnEnable()
        {
            ClearInventory();
            CreateSlots();
            SharpsCountText.text = _sharps.Value.ToString(CultureInfo.InvariantCulture); 
        }

        private void CreateSlots()
        {
            _noItems.SetActive(true);
            foreach (var cell in Inventory.MyInventory)
            {
                var currentItem = cell.Item as IUpgradableItem;

                if (currentItem?.ItemsForLevels == null ||
                    currentItem.ThisItemLevel == 0 ||
                    currentItem.ThisItemLevel >= currentItem.ItemsForLevels.Count ||
                    !(_sharps.Value >= ((IUpgradableItem)currentItem.ItemsForLevels[currentItem.ThisItemLevel]).CostForThisItem)) continue;
            
                var rawSlot = Instantiate(_slotPrefab, transform.position, Quaternion.identity, transform);
                rawSlot.GetComponent<UpgradeSlot>().Setup(new Cell(currentItem.ItemsForLevels[currentItem.ThisItemLevel]), this, _player);
                _noItems.SetActive(false);
            }
        }

        private void ClearInventory()
        {
            for (var i = 0; i < transform.childCount; i++)
                Destroy(transform.GetChild(i).gameObject);
        }

        public void Close()
        {
            UnityEngine.Time.timeScale = 1;
            gameObject.transform.parent.parent.parent.parent.gameObject.SetActive(false);
            _uniqueSetup.SetupUnique(_player);
        }
    }
}
