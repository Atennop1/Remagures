using Remagures.Model.Damage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class PhysicsDamageSetuper : SerializedMonoBehaviour
    {
        [SerializeField] private IDamageFactory _damageFactory;
        [SerializeField] private PhysicsDamage _physicsDamage;

        private void Awake()
            => _physicsDamage.Construct(_damageFactory.Create());
    }
}