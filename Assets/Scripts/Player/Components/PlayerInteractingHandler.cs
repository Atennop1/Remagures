using Remagures.AI.NPCs.Components;
using Remagures.DialogSystem.UI;
using Remagures.Interactable;
using Remagures.Interactable.Abstraction;
using Remagures.SO.Other;
using Remagures.SO.PlayerStuff;
using UnityEngine;

namespace Remagures.Player.Components
{
    public enum InteractingState
    {
        None,
        Ready,
        Interact
    }

    public class PlayerInteractingHandler : MonoBehaviour, IPlayerComponent
    {
        [SerializeField] private Player _player;
        [SerializeField] private DialogView _dialogView;

        [Space]
        [SerializeField] private Signal _detectInteractSignal;
        [SerializeField] private ItemValue _currentItem;

        [Space]
        [SerializeField] private SpriteRenderer _receivedItemSprite;
        [SerializeField] private GameObject _dialogWindow;
        [SerializeField] private UIActivityChanger _uiActivityChanger;

        public bool CanShowContextClue { get; private set; } = true;
        public InteractingState CurrentState { get; private set; }
        public Remagures.Interactable.Abstraction.Interactable CurrentInteractable { get; private set; }

        private const string RECEIVING_ANIMATOR_NAME = "receiving";
        private PlayerAnimations _playerAnimations;

        public void RaiseItem()
        {
            if (_player.CurrentState == PlayerState.Interact || _currentItem.Value == null) return;
        
            _playerAnimations.ChangeAnim(RECEIVING_ANIMATOR_NAME, true);
            _player.ChangeState(PlayerState.Interact);
            _receivedItemSprite.sprite = _currentItem.Value.ItemSprite;
        }

        public void Interact()
        {
            CurrentState = InteractingState.Interact;
            CurrentInteractable.Interact();
        
            CanShowContextClue = false;
            CurrentInteractable.ContextClue.ChangeContext();
        }

        public void DialogOnTaped()
        {
            if ((CurrentInteractable is not InteractableWithTextDisplay { CanContinue: true } && 
                CurrentInteractable is not NPC) ||
                !_dialogView.IsDialogEnded ||
                !_dialogView.CanContinue) return;
        
            CurrentInteractable = null;
            CanShowContextClue = true;
            CurrentState = InteractingState.None;
            _detectInteractSignal.Invoke();

            _player.ChangeState(PlayerState.Idle);
            _receivedItemSprite.sprite = null;
            _currentItem.Value = null;
            
            _playerAnimations.ChangeAnim(RECEIVING_ANIMATOR_NAME, false);
            _dialogWindow.SetActive(false);
            _uiActivityChanger.TurnOn();
        }

        public void SetCurrentInteractable(Remagures.Interactable.Abstraction.Interactable interactable)
        {
            if (interactable.PlayerInRange && CurrentInteractable == null)
                CurrentInteractable = interactable;
        
            SetCurrentState(interactable);
        }

        public void ResetCurrentInteractable(Remagures.Interactable.Abstraction.Interactable interactable)
        {
            if (!interactable.PlayerInRange && CurrentState == InteractingState.Ready && CurrentInteractable == interactable)
                CurrentInteractable = null;

            SetCurrentState(interactable);
            _detectInteractSignal.Invoke();
        }
        
        private void SetCurrentState(Remagures.Interactable.Abstraction.Interactable interactable)
        {
            if (CurrentState == InteractingState.Interact ||
                (CurrentInteractable != null && CurrentInteractable != interactable)) return;

            CurrentState = interactable.PlayerInRange ? InteractingState.Ready : InteractingState.None;
        }

        private void Awake()
        {
            _playerAnimations = _player.GetPlayerComponent<PlayerAnimations>();
        }
    }
}