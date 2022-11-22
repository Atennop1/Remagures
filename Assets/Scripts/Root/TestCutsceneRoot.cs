using System.Collections.Generic;
using Remagures.Cutscenes;
using Remagures.Cutscenes.Actions;
using Remagures.DialogSystem.UI;
using Remagures.Player.Components;
using Remagures.Root.SystemUpdates;
using UnityEngine;

namespace Remagures.Root
{
    public class TestCutsceneRoot : MonoBehaviour, ICompositeRoot
    {
        [SerializeField] private DialogTypeWriter _writer;
        [SerializeField] private PlayerMovement _playerMovement;
        private ISystemUpdate _systemUpdate;
        
        public void Compose()
        {
            _systemUpdate = new SystemUpdate();
            var actions = new List<ICutsceneAction>
            {
                new TeleportAction(_playerMovement.transform, new Vector2(-6.5f, 5)),
                
                new TimerAction(1.5f),
                new MoveAction(_playerMovement, new Vector2(0, 5)),
                
                new TimerAction(1.5f),
                new MoveAction(_playerMovement, new Vector2(0, 0)),
                
                new TimerAction(0.5f),
                new DialogAction(_writer, "Где я?"),
                new DialogAction(_writer, "Что это за место?"),
                new DialogAction(_writer, "Что происходит вообще?"),
            };
            
            var cutscene = new Cutscene(actions);
            _systemUpdate.Add(cutscene);
        }

        private void Update() => _systemUpdate.UpdateAll();
    }
}