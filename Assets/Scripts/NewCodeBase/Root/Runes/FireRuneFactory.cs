using Remagures.Model.RuneSystem;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class FireRuneFactory : MonoBehaviour, IRuneFactory
    {
        private IRune _builtRune;
        
        public IRune Create()
        {
            if (_builtRune != null)
                return _builtRune;
            
            _builtRune = new FireRune();
            return _builtRune;
        }
    }
}