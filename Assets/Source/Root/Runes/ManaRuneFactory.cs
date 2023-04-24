using Remagures.Model.RuneSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ManaRuneFactory : SerializedMonoBehaviour, IRuneFactory
    {
        [SerializeField] private IManaFactory _manaFactory;
        private IRune _builtRune;
        
        public IRune Create()
        {
            if (_builtRune != null)
                return _builtRune;

            _builtRune = new ManaRune(_manaFactory.Create());
            return _builtRune;
        }
    }
}