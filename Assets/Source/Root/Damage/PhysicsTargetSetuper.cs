using Remagures.Model.Damage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class PhysicsTargetSetuper : SerializedMonoBehaviour
    {
        [SerializeField] private ITargetFactory _targetFactory;
        [SerializeField] private PhysicsTarget _physicsTarget;
        
        private void Awake() 
            => _physicsTarget.Construct(_targetFactory.Create());
    }
}