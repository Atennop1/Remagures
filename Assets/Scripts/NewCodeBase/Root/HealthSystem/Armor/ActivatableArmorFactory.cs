using Remagures.Model.Health;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ActivatableArmorFactory : MonoBehaviour, IArmorFactory
    {
        [SerializeField] private float _armorCoefficient;
        [SerializeField] private ArmorFactory _armorFactory;
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