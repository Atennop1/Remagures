using Remagures.Model.Knockback;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class KnockableFactory : SerializedMonoBehaviour, IKnockableFactory
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private LayerMask _layerMask;

        public IKnockable Create()
            => new Knockable(_rigidbody, _layerMask);
    }
}