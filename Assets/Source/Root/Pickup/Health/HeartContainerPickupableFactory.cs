using Remagures.Model.Pickup;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.Health
{
    public sealed class HeartContainerPickupableFactory : SerializedMonoBehaviour, IPickupableFactory
    {
        [SerializeField] private IHealthFactory _healthFactory;
        [SerializeField] private IMaxHealthFactory _maxHealthFactory;

        public IPickupable Create()
            => new HeartContainerPickupable(_healthFactory.Create(), _maxHealthFactory.Create());
    }
}