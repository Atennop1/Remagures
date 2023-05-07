using Remagures.Model.Damage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class TargetFactory : SerializedMonoBehaviour, ITargetFactory
    {
        [SerializeField] private IHealthFactory _healthFactory;
        [SerializeField] private FlashingableFactory _flashingableFactory;
        private ITarget _builtTarget;
        
        public ITarget Create()
        {
            if (_builtTarget != null)
                return _builtTarget;
            
            _builtTarget = new Target(_healthFactory.Create(), _flashingableFactory.Create());
            return _builtTarget;
        }
    }
}