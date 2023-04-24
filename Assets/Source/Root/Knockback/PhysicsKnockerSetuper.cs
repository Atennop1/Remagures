using Remagures.Model.Knockback;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class PhysicsKnockerSetuper : SerializedMonoBehaviour
    {
        [SerializeField] private IKnockerFactory _knockerFactory;
        [SerializeField] private PhysicsKnocker _physicsKnocker;

        private void Awake()
            => _physicsKnocker.Construct(_knockerFactory.Create());
    }
}