using System.Collections.Generic;
using Remagures.Cutscenes;
using Remagures.Cutscenes.Actions;
using Remagures.DialogSystem.View;
using Remagures.Interactable;
using Remagures.Player.Components;
using Remagures.Root.SystemUpdates;
using UnityEngine;

namespace Remagures.Root
{
    public class TestCutsceneRoot : CompositeRoot
    {
        [SerializeField] private DialogTypeWriter _writer;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private UIActivityChanger _uiActivityChanger;
        private ISystemUpdate _systemUpdate;

        public override void Compose()
        {
            _systemUpdate = new SystemUpdate();
            var actions = new List<ICutsceneAction>
            {
                new StartAction(_uiActivityChanger),
                new TeleportAction(_playerMovement.transform, new Vector2(-6.5f, 5)),
                
                new TimerAction(1.5f),
                new MoveAction(_playerMovement, new Vector2(0, 5)),
                new MoveAction(_playerMovement, new Vector2(0, 4.99f)),
                
                new TimerAction(1.5f),
                new MoveAction(_playerMovement, new Vector2(0, 0)),
                
                new TimerAction(0.5f),
                new DialogAction(_writer, "Где я?"),
                new DialogAction(_writer, "Что это за место?"),
                new DialogAction(_writer, "Что происходит вообще?"),
                
                new EndDialogAction(_writer),
                new EndAction(_uiActivityChanger)
            };
            
            var cutscene = new Cutscene(actions);
            _systemUpdate.Add(cutscene);
        }

        private void FixedUpdate() => _systemUpdate.UpdateAll();
    }
}