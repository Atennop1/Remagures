using Remagures.View.UpgradeSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Factories
{
    public sealed class UpgradeSlotsFactory : SerializedMonoBehaviour, IUpgradeSlotsViewFactory
    {
        [SerializeField] private GameObject _slotPrefab;

        public IUpgradeSlotView Create(Transform content) 
            => Instantiate(_slotPrefab, content).GetComponent<IUpgradeSlotView>();
    }
}