using Remagures.Model.Health;
using Remagures.View.Health;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class HealthFactory : MonoBehaviour, IHealthFactory
    {
        [SerializeField] private IHealthView _healthView;
        [SerializeField] private IMaxHealthFactory _maxHealthFactory;
        private IHealth _builtHealth;

        public IHealth Create()
        {
            if (_builtHealth != null)
                return _builtHealth;
            
            _builtHealth = new Health(_healthView, _maxHealthFactory.Create());
            return _builtHealth;
        }
    }
}