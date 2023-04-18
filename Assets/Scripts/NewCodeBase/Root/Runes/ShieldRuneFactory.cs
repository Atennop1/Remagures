using Remagures.Model.RuneSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ShieldRuneFactory : SerializedMonoBehaviour, IRuneFactory
    {
        [SerializeField] private ActivatableArmorValueFactory _armorValueFactory;
        private IRune _builtRune;
        
        public IRune Create()
        {
            if (_builtRune != null)
                return _builtRune;

            _builtRune = new ShieldRune(_armorValueFactory.Create());
            return _builtRune;
        }
    }
}