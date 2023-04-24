using Remagures.Model.Character;
using Remagures.Model.SceneTransition;
using SaveSystem;
using SaveSystem.Paths;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class TransitionWithPlayerPositionSavingFactory : SerializedMonoBehaviour, ISceneTransitionFactory
    {
        [SerializeField] private ISceneTransitionFactory _transitionFactory;
        [SerializeField] private CharacterMovement _characterMovement;
        private ISceneTransition _builtTransition;
        
        public ISceneTransition Create()
        {
            if (_builtTransition != null)
                return _builtTransition;

            var storage = new BinaryStorage<CharacterPositionData>(new Path("CharacterPositionData"));
            _builtTransition = new TransitionWithPlayerPositionSaving(_transitionFactory.Create(), _characterMovement, storage);
            return _builtTransition;
        }
    }
}