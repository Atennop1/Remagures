using Remagures.Model.Magic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MagicFactory : SerializedMonoBehaviour, IMagicFactory
    {
        [SerializeField] private IManaFactory _manaFactory;
        [SerializeField] private IMagicData _magicData;
        private IMagic _builtMagic;

        public IMagic Create()
        {
            if (_builtMagic != null)
                return _builtMagic;

            _builtMagic = new Magic(_manaFactory.Create(), _magicData);
            return _builtMagic;
        }
    }
}