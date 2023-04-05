using Remagures.Model.Pickup;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class HeartPickupableFactory : SerializedMonoBehaviour, IPickupableFactory
    {
        [SerializeField] private IHealthFactory _healthFactory;

        public IPickupable Create()
            => new HeartPickupable(_healthFactory.Create());
    }
}