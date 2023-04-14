using Remagures.Model.Magic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MaxManaFactory : SerializedMonoBehaviour, IMaxManaFactory
    {
        [SerializeField] private int _currentValue;
        [SerializeField] private int _maxValue;
        private IMaxMana _builtMaxMana;
        
        public IMaxMana Create()
        {
            if (_builtMaxMana != null)
                return _builtMaxMana;

            _builtMaxMana = new MaxMana(_currentValue, _maxValue);
            return _builtMaxMana;
        }
    }
}