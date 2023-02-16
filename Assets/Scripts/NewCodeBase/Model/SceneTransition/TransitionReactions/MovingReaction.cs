using System;
using System.Collections.Generic;
using Remagures.Model.Character;
using Remagures.Model.CutscenesSystem;
using Remagures.Root;
using Remagures.View;
using UnityEngine;

namespace Remagures.Model.SceneTransition
{
    public class MovingReaction : IUpdatable
    {
        private readonly ISceneTransition _transition;
        private readonly PlayerMovement _playerMovement;
        private readonly UIActivityChanger _uiActivityChanger;
        
        private readonly ISystemUpdate _systemUpdate = new SystemUpdate();

        public MovingReaction(ISceneTransition transition, PlayerMovement playerMovement, UIActivityChanger uiActivityChanger)
        {
            _transition = transition ?? throw new ArgumentNullException(nameof(transition));
            _playerMovement = playerMovement ?? throw new ArgumentNullException(nameof(playerMovement));
            _uiActivityChanger = uiActivityChanger ?? throw new ArgumentNullException(nameof(uiActivityChanger));
        }

        public void Update()
        {
            _systemUpdate.UpdateAll();

            if (_transition.HasActivated)
                StartMovingCutscene();
        }
        
        private void StartMovingCutscene()
        {
            var moveTo = (Vector2)_playerMovement.transform.position + _playerMovement.PlayerViewDirection * 2;
            
            var transitingCutscene = new Cutscene(new List<ICutsceneAction>
            {
                new StartAction(_uiActivityChanger),
                new MoveAction(_playerMovement, moveTo)
            });
            
            _systemUpdate.Add(transitingCutscene);
            transitingCutscene.Start();
        }
    }
}