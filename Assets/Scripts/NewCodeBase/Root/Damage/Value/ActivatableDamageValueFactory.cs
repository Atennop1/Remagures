using Remagures.Model.Damage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ActivatableDamageValueFactory : SerializedMonoBehaviour, IDamageValueFactory
    {
        [SerializeField] private IDamageValueFactory _originDamageValueFactory;
        [SerializeField] private IDamageValueFactory _activatingDamageValueFactory;
        private ActivatableDamageValue _builtDamageValue;

        IDamageValue IDamageValueFactory.Create()
            => Create();
        
        public ActivatableDamageValue Create()
        {
            if (_builtDamageValue != null)
                return _builtDamageValue;

            _builtDamageValue = new ActivatableDamageValue(_originDamageValueFactory.Create(), _activatingDamageValueFactory.Create());
            return _builtDamageValue;
        }
    }
}