using Remagures.Model.Character;
using Remagures.Model.Magic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class ProjectileMagicFactory : SerializedMonoBehaviour, IMagicFactory
    {
        [SerializeField] private IMagicFactory _magicFactory;
        [SerializeField] private Factories.IProjectileFactory _projectileFactory;
        [SerializeField] private CharacterMovement _characterMovement;
        private IMagic _builtMagic;

        public IMagic Create()
        {
            if (_builtMagic != null)
                return _builtMagic;

            _builtMagic = new ProjectileMagic(_magicFactory.Create(), _projectileFactory, _characterMovement);
            return _builtMagic;
        }
    }
}