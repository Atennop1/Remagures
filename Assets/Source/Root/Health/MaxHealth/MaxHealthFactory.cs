using Remagures.Model.Health;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MaxHealthFactory : SerializedMonoBehaviour, IMaxHealthFactory
    {
        [SerializeField] private int _currentMaxHealth;
        [SerializeField] private int _maxPossibleHealth;
        private IMaxHealth _builtHealth;

        public IMaxHealth Create()
        {
            if (_builtHealth != null)
                return _builtHealth;

            _builtHealth = new MaxHealth(_currentMaxHealth, _maxPossibleHealth);
            return _builtHealth;
        }
    }
}