using Remagures.Model;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ArmorFactory : MonoBehaviour, IArmorFactory
    {
        [SerializeField] private IArmorValueFactory _armorValueFactory;
        private Armor _builtArmor;

        IArmor IArmorFactory.Create()
            => Create();
        
        public Armor Create()
        {
            if (_builtArmor != null)
                return _builtArmor;

            _builtArmor = new Armor(_armorValueFactory.Create());
            return _builtArmor;
        }
    }
}