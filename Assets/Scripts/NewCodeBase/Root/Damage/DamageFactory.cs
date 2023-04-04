using Remagures.Model.Damage;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class DamageFactory : SerializedMonoBehaviour, IDamageFactory
    {
        [SerializeField] private int _value;
        
        public IDamage Create() 
            => new Damage(_value);
    }
}