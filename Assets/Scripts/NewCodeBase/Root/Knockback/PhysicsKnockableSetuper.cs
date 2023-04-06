using Remagures.Model.Knockback;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class PhysicsKnockableSetuper : SerializedMonoBehaviour
    {
        [SerializeField] private IKnockableFactory _knockableFactory;
        [SerializeField] private PhysicsKnockable _physicsKnockable;

        private void Awake()
            => _physicsKnockable.Construct(_knockableFactory.Create());
    }
}