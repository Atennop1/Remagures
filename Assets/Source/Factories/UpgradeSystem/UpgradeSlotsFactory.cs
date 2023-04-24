using Remagures.View.UpgradeSystem;
using UnityEngine;

namespace Remagures.Factories
{
    public sealed class UpgradeSlotsFactory : MonoBehaviour, IUpgradeSlotsViewFactory
    {
        [SerializeField] private GameObject _slotPrefab;

        public IUpgradeSlotView Create(Transform content)
        {   
            return Instantiate(_slotPrefab, content).GetComponent<IUpgradeSlotView>();
        }
    }
}