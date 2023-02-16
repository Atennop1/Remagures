using System;
using Remagures.Model.Character;
using Remagures.Root;

namespace Remagures.Model.SceneTransition
{
    public class SavePlayerPositionReaction : IUpdatable
    {
        private readonly ISceneTransition _sceneTransition;
        private readonly PlayerMovement _playerMovement;
        private readonly CharacterPositionStorage _characterPositionStorage;

        public SavePlayerPositionReaction(ISceneTransition sceneTransition, PlayerMovement playerMovement, CharacterPositionStorage characterPositionStorage)
        {
            _sceneTransition = sceneTransition ?? throw new ArgumentNullException(nameof(sceneTransition));
            _playerMovement = playerMovement ?? throw new ArgumentNullException(nameof(playerMovement));
            _characterPositionStorage = characterPositionStorage ?? throw new ArgumentNullException(nameof(characterPositionStorage));
        }

        public void Update()
        {
            if (!_sceneTransition.HasActivated) 
                return;
            
            var data = new CharacterPositionData(_playerMovement.transform.position, _playerMovement.PlayerViewDirection);
            _characterPositionStorage.Save(data);
        }
    }
}