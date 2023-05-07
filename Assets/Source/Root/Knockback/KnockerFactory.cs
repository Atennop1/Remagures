using Remagures.Model.Knockback;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class KnockerFactory : SerializedMonoBehaviour, IKnockerFactory
    {
        [SerializeField] private float _strength;
        [SerializeField] private int _knockTimeInMilliseconds;
        private IKnocker _builtKnocker;

        public IKnocker Create()
        {
            if (_builtKnocker != null)
                return _builtKnocker;
            
            _builtKnocker = new Knocker(_strength, _knockTimeInMilliseconds);
            return _builtKnocker;
        }
    }
}