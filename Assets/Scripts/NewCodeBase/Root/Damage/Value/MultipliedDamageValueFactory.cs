using Remagures.Model.Damage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MultipliedDamageValueFactory : SerializedMonoBehaviour, IDamageValueFactory
    {
        [SerializeField] private int _multiplier;
        [SerializeField] private IDamageValueFactory _damageValueFactory;
        private IDamageValue _builtDamageValue;
        
        public IDamageValue Create()
        {
            if (_builtDamageValue != null)
                return _builtDamageValue;

            _builtDamageValue = new MultipliedDamageValue(_damageValueFactory.Create(), _multiplier);
            return _builtDamageValue;
        }
    }
}