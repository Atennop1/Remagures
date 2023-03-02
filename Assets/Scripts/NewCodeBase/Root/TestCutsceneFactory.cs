using System.Collections.Generic;
using Remagures.Model.Character;
using Remagures.Model.CutscenesSystem;
using Remagures.Model.DialogSystem;
using Remagures.View;
using Remagures.View.DialogSystem;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace Remagures.Root
{
    public sealed class TestCutsceneFactory : SerializedMonoBehaviour
    {
        [Header("Dialogs Stuff")] 
        [SerializeField] private DialogView _dialogView;
        [SerializeField] private TextWriter _writer;
        [SerializeField] private Button _dialogWindowButton;
        [SerializeField] private Text _continueText;
        
        [Space]
        [SerializeField] private CharacterMovement characterMovement;
        [SerializeField] private UIActivityChanger _uiActivityChanger;
        private ISystemUpdate _systemUpdate;

        public ICutscene Create()
        {
            _systemUpdate = new SystemUpdate();
            var actions = new List<ICutsceneAction>
            {
                new StartAction(_uiActivityChanger),
                new TeleportAction(characterMovement.Transform, new Vector2(-6.5f, 5)),
                
                new TimerAction(1.5f),
                new MoveAction(characterMovement, new Vector2(0, 5)),
                new MoveAction(characterMovement, new Vector2(0, 4.99f)),
                
                new TimerAction(1.5f),
                new MoveAction(characterMovement, new Vector2(0, 0)),
                
                new TimerAction(0.5f),
                new DialogAction(_writer, _dialogWindowButton, _continueText, "Где я?"),
                new DialogAction(_writer, _dialogWindowButton, _continueText, "Что это за место?"),
                new DialogAction(_writer, _dialogWindowButton, _continueText, "Что происходит вообще?"),
                
                new EndDialogAction(_dialogView),
                new EndAction(_uiActivityChanger)
            };
            
            var cutscene = new Cutscene(actions);
            _systemUpdate.Add(cutscene);
            return cutscene;
        }

        private void FixedUpdate() 
            => _systemUpdate.UpdateAll();
    }
}