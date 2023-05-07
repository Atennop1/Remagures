using Remagures.Model.Knockback;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class KnockableFactory : SerializedMonoBehaviour, IKnockableFactory
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private LayerMask _layerMask;
        private IKnockable _builtKnockable;
        
        public IKnockable Create()
        {
            if (_builtKnockable != null)
                return _builtKnockable;
            
            _builtKnockable = new Knockable(_rigidbody, _layerMask);
            return _builtKnockable;
        }
    }
}