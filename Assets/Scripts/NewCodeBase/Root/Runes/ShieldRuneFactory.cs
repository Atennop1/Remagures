using Remagures.Model.RuneSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ShieldRuneFactory : SerializedMonoBehaviour, IRuneFactory
    {
        [SerializeField] private ActivatableArmorFactory _armorFactory;
        private IRune _builtRune;
        
        public IRune Create()
        {
            if (_builtRune != null)
                return _builtRune;

            _builtRune = new ShieldRune(_armorFactory.Create());
            return _builtRune;
        }
    }
}