using Remagures.Model.Health;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class HealthFactory : MonoBehaviour, IHealthFactory
    {
        [SerializeField] private int _value;
        private IHealth _builtHealth;

        public IHealth Create()
        {
            if (_builtHealth != null)
                return _builtHealth;
            
            _builtHealth = new Health(_value);
            return _builtHealth;
        }
    }
}