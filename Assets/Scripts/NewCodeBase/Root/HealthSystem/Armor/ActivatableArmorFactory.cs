using Remagures.Model.Health;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ActivatableArmorFactory : SerializedMonoBehaviour, IArmorFactory
    {
        [SerializeField] private float _armorCoefficient;
        [SerializeField] private IArmorFactory _armorFactory;
        private ActivatableArmor _builtArmor;

        IArmor IArmorFactory.Create()
            => Create();
        
        public ActivatableArmor Create()
        {
            if (_builtArmor != null)
                return _builtArmor;

            _builtArmor = new ActivatableArmor(_armorFactory.Create(), _armorCoefficient);
            return _builtArmor;
        }
    }
}