using Remagures.Model.Health;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class HealthFactory : MonoBehaviour
    {
        [SerializeField] private int _value;
        [SerializeField] private IArmorFactory _armorFactory;
        private IHealth _builtHealth;

        public IHealth Create()
        {
            if (_builtHealth != null)
                return _builtHealth;
            
            _builtHealth = new HealthWithArmor(new Health(_value), _armorFactory.Create());
            return _builtHealth;
        }
    }
}