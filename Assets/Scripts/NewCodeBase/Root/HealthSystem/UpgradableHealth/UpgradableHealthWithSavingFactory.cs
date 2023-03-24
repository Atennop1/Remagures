using Remagures.Model.Health;
using SaveSystem;
using SaveSystem.Paths;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class UpgradableHealthWithSavingFactory : SerializedMonoBehaviour, IUpgradableHealthFactory, IHealthFactory
    {
        [SerializeField] private IUpgradableHealthFactory _upgradableHealthFactory;
        private IUpgradableHealth _builtHealth;
        
        IHealth IHealthFactory.Create() 
            => Create();
        
        public IUpgradableHealth Create()
        {
            if (_builtHealth != null)
                return _builtHealth;

            _builtHealth = new UpgradableHealthWithSaving(_upgradableHealthFactory.Create(), new BinaryStorage<int>(new Path("PlayerMaxHealth")));
            return _builtHealth;
        }
    }
}