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
        private readonly CharacterMovement _characterMovement;
        private readonly UIActivityChanger _uiActivityChanger;
        
        private readonly ISystemUpdate _systemUpdate = new SystemUpdate();

        public MovingReaction(ISceneTransition transition, CharacterMovement characterMovement, UIActivityChanger uiActivityChanger)
        {
            _transition = transition ?? throw new ArgumentNullException(nameof(transition));
            _characterMovement = characterMovement ?? throw new ArgumentNullException(nameof(characterMovement));
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
            var moveTo = (Vector2)_characterMovement.Transform.position + _characterMovement.CharacterLookDirection * 2;
            
            var transitingCutscene = new Cutscene(new List<ICutsceneAction>
            {
                new StartAction(_uiActivityChanger),
                new MoveAction(_characterMovement, moveTo)
            });
            
            _systemUpdate.Add(transitingCutscene);
            transitingCutscene.Start();
        }
    }
}