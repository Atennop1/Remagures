using Remagures.Model.Health;
using SaveSystem;
using SaveSystem.Paths;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MaxHealthWithSavingFactory : SerializedMonoBehaviour, IMaxHealthFactory
    {
        [SerializeField] private IMaxHealthFactory _maxHealthFactory;
        private IMaxHealth _builtHealth;

        public IMaxHealth Create()
        {
            if (_builtHealth != null)
                return _builtHealth;

            _builtHealth = new MaxHealthWithSaving(_maxHealthFactory.Create(), new BinaryStorage<int>(new Path("MaxPlayerHealth")));
            return _builtHealth;
        }
    }
}