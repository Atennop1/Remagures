using Remagures.Model;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ActivatableArmorValueFactory : SerializedMonoBehaviour, IArmorValueFactory
    {
        [SerializeField] private IArmorValueFactory _originArmorValueFactory;
        [SerializeField] private IArmorValueFactory _activatingArmorValueFactory;
        private ActivatableArmorValue _builtValue;

        IArmorValue IArmorValueFactory.Create()
            => Create();
        
        public ActivatableArmorValue Create()
        {
            if (_builtValue != null)
                return _builtValue;

            _builtValue = new ActivatableArmorValue(_originArmorValueFactory.Create(), _activatingArmorValueFactory.Create());
            return _builtValue;
        }
    }
}