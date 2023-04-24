using Remagures.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ArmorValueFactory : SerializedMonoBehaviour, IArmorValueFactory
    {
        [SerializeField] private float _value;
        private ArmorValue _builtValue;

        IArmorValue IArmorValueFactory.Create()
            => Create();
        
        public ArmorValue Create()
        {
            if (_builtValue != null)
                return _builtValue;

            _builtValue = new ArmorValue(_value);
            return _builtValue;
        }
    }
}