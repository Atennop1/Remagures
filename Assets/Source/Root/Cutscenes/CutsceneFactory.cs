using System.Collections.Generic;
using System.Linq;
using Remagures.Model.CutscenesSystem;
using Remagures.Root.Actions;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Remagures.Root
{
    public sealed class CutsceneFactory : SerializedMonoBehaviour, ICutsceneFactory
    {
        [SerializeField] private List<ICutsceneActionFactory> _cutsceneActionFactories;

        private Cutscene _builtCutscene;
        private readonly ISystemUpdate _systemUpdate = new SystemUpdate();

        public ICutscene Create()
        {
            if (_builtCutscene != null)
                return _builtCutscene;
            
            var actions = _cutsceneActionFactories.Select(factory => factory.Create()).ToList();
            _builtCutscene = new Cutscene(actions);
            
            _systemUpdate.Add(_builtCutscene);
            return _builtCutscene;
        }

        private void FixedUpdate() 
            => _systemUpdate.UpdateAll();
    }
}











/*
 Actions for start cutscene:
 
 new StartAction(_uiActivityChanger),
 new TeleportAction(characterMovement.Transform, new Vector2(-6.5f, 5)),
                
 new WaitAction(1.5f),
 new MoveAction(characterMovement, new Vector2(0, 5)),
 new MoveAction(characterMovement, new Vector2(0, 4.99f)),
                
 new WaitAction(1.5f),
 new MoveAction(characterMovement, new Vector2(0, 0)),
                
 new WaitAction(0.5f),
 new DialogAction(_writer, _dialogWindowButton, _continueText, "Где я?"),
 new DialogAction(_writer, _dialogWindowButton, _continueText, "Что это за место?"),
 new DialogAction(_writer, _dialogWindowButton, _continueText, "Что происходит вообще?"),
                
 new CloseDialogWindowDialogAction(_dialogView),
 new EndAction(_uiActivityChanger)
 
*/