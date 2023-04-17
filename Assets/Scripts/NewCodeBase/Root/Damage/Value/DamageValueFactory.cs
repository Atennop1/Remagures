using Remagures.Model.Damage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class DamageValueFactory : SerializedMonoBehaviour, IDamageValueFactory
    {
        [SerializeField] private int _value;
        private IDamageValue _builtDamageValue;
        
        public IDamageValue Create()
        {
            if (_builtDamageValue != null)
                return _builtDamageValue;

            _builtDamageValue = new DamageValue(_value);
            return _builtDamageValue;
        }
    }
}