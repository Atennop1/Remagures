using Remagures.Model.Knockback;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class KnockerFactory : SerializedMonoBehaviour, IKnockerFactory
    {
        [SerializeField] private float _strength;
        [SerializeField] private int _knockTimeInMilliseconds;

        public IKnocker Create()
            => new Knocker(_strength, _knockTimeInMilliseconds);
    }
}