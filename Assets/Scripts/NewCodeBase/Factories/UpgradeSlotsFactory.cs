using UnityEngine;

namespace Remagures.Factories
{
    public sealed class UpgradeSlotsFactory : MonoBehaviour, IUpgradeSlotsViewFactory
    {
        [SerializeField] private GameObject _slotPrefab;

        public IUpgradeSlotsView Create()
        {
            
        }
    }
}