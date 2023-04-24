using Remagures.Model.Damage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class TargetFactory : SerializedMonoBehaviour, ITargetFactory
    {
        [SerializeField] private IHealthFactory _healthFactory;
        [SerializeField] private FlashingableFactory _flashingableFactory;
        
        public ITarget Create()
            => new Target(_healthFactory.Create(), _flashingableFactory.Create());
    }
}