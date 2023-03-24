using Remagures.Model.Health;
using SaveSystem;
using SaveSystem.Paths;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class HealthWithSavingFactory : SerializedMonoBehaviour, IHealthFactory
    {
        [SerializeField] private IHealthFactory _healthFactory;
        private IHealth _builtHealth;

        public IHealth Create()
        {
            if (_builtHealth != null)
                return _builtHealth;
            
            _builtHealth = new HealthWithSaving(_healthFactory.Create(), new BinaryStorage<int>(new Path("PlayerHealth")));
            return _builtHealth;
        }
    }
}