using Remagures.Model.Health;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class HealthWithArmorFactory : SerializedMonoBehaviour, IHealthFactory
    {
        [SerializeField] private IArmorFactory _armorFactory;
        [SerializeField] private IHealthFactory _healthFactory;
        private IHealth _builtHealth;
        
        public IHealth Create()
        {
            if (_builtHealth != null)
                return _builtHealth;

            _builtHealth = new HealthWithArmor(_healthFactory.Create(), _armorFactory.Create());
            return _builtHealth;
        }
    }
}