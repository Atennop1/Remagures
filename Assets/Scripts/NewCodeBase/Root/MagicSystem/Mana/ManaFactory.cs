using Remagures.Model.Magic;
using Remagures.View.Mana;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ManaFactory : SerializedMonoBehaviour, IManaFactory
    {
        [SerializeField] private IMaxManaFactory _maxManaFactory;
        [SerializeField] private IManaView _manaView;
        private IMana _builtMana;
        
        public IMana Create()
        {
            if (_builtMana != null)
                return _builtMana;
            
            _builtMana = new Mana(_manaView, _maxManaFactory.Create());
            return _builtMana;
        }
    }
}