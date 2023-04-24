using Remagures.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MultipliedArmorValueFactory : SerializedMonoBehaviour, IArmorValueFactory
    {
        [SerializeField] private int _multiplier;
        [SerializeField] private IArmorValueFactory _armorValueFactory;
        private IArmorValue _builtValue;
        
        public IArmorValue Create()
        {
            if (_builtValue != null)
                return _builtValue;

            _builtValue = new MultipliedArmorValue(_armorValueFactory.Create(), _multiplier);
            return _builtValue;
        }
    }
}