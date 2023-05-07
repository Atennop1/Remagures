using Remagures.Model.Health;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class PhysicsHealthSetuper : SerializedMonoBehaviour
    {
        [SerializeField] private IHealthFactory _healthFactory;
        [SerializeField] private PhysicsHealth _physicsHealth;

        private void Awake() 
            => _physicsHealth.Construct(_healthFactory.Create());
    }
}