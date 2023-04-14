using System;
using Remagures.Model.Character;
using SaveSystem;

namespace Remagures.Model.SceneTransition
{
    public sealed class TransitionWithPlayerPositionSaving : ISceneTransition
    {
        private readonly ISceneTransition _sceneTransition;
        private readonly CharacterMovement _characterMovement;
        private readonly ISaveStorage<CharacterPositionData> _characterPositionStorage;

        public TransitionWithPlayerPositionSaving(ISceneTransition sceneTransition, CharacterMovement characterMovement, ISaveStorage<CharacterPositionData> characterPositionStorage)
        {
            _sceneTransition = sceneTransition ?? throw new ArgumentNullException(nameof(sceneTransition));
            _characterMovement = characterMovement ?? throw new ArgumentNullException(nameof(characterMovement));
            _characterPositionStorage = characterPositionStorage ?? throw new ArgumentNullException(nameof(characterPositionStorage));
        }

        public void Activate()
        {
            _sceneTransition.Activate();
            var data = new CharacterPositionData(_characterMovement.Transform.position, _characterMovement.CharacterLookDirection);
            _characterPositionStorage.Save(data);
        }
    }
}