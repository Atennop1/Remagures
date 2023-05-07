using Remagures.Model.Damage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class DamageFactory : SerializedMonoBehaviour, IDamageFactory
    {
        [SerializeField] private IDamageValueFactory _damageValueFactory;
        private IDamage _builtDamage;
        
        public IDamage Create()
        {
            if (_builtDamage != null)
                return _builtDamage;
            
            _builtDamage = new Damage(_damageValueFactory.Create());
            return _builtDamage;
        }
    }
}