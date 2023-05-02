using Remagures.Model.Character;
using Remagures.Model.Damage;
using SaveSystem;
using SaveSystem.Paths;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MultipliedDamageValuerForClassFactory : SerializedMonoBehaviour, IDamageValueFactory
    {
        [SerializeField] private int _multiplier;
        [SerializeField] private CharacterClass _characterClass;
        [SerializeField] private IDamageValueFactory _damageValueFactory;
        
        private readonly ISaveStorage<CharacterClass> _characterClassStorage = new BinaryStorage<CharacterClass>(new Path("CharacterClass"));
        private IDamageValue _builtDamageValue;
        
        public IDamageValue Create()
        {
            if (!_characterClassStorage.HasSave() || _characterClassStorage.Load() != _characterClass)
                return _damageValueFactory.Create();
            
            if (_builtDamageValue != null)
                return _builtDamageValue;

            _builtDamageValue = new MultipliedDamageValue(_damageValueFactory.Create(), _multiplier);
            return _builtDamageValue;
        }
    }
}