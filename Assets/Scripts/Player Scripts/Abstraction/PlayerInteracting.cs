using UnityEngine;
using UnityEngine.Playables;

public enum InteractingState
{
    None,
    Ready,
    Interact
}

public class PlayerInteracting : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private DialogView _dialogView;

    [Space]
    [SerializeField] private Signal _detectInteractSignal;
    [SerializeField] private ItemValue _currentItem;

    [Space]
    [SerializeField] private SpriteRenderer _receivedItemSprite;
    [SerializeField] private GameObject _dialogWindow;

    public bool CanShowContextClue { get; private set; } = true;
    public InteractingState CurrentState { get; private set; }
    public Interactable CurrentInteractable { get; private set; }

    private const string RECEIVING_ANIMATOR_NAME = "receiving";

    public void RaiseItem()
    {
        if (_player.CurrentState != PlayerState.Interact && _currentItem.Value != null)
        {
            _player.PlayerAnimations.ChangeAnim(RECEIVING_ANIMATOR_NAME, true);
            _player.ChangeState(PlayerState.Interact);
            _receivedItemSprite.sprite = _currentItem.Value.ItemSprite;
        }
    }

    public void Interact()
    {
        CurrentState = InteractingState.Interact;
        CurrentInteractable?.Interact();
        CanShowContextClue = false;
        CurrentInteractable.Context.Invoke();
    }

    public void DialogOnTaped()
    {
        if (((CurrentInteractable is InteractableWithTextDisplay && (CurrentInteractable as InteractableWithTextDisplay).CanContinue) || 
        (TimelineView.Instance != null && TimelineView.Instance.IsPlaying && TimelineView.Instance.CanContinue) ||
        CurrentInteractable is NPC) &&
        _dialogView.IsDialogEnded && 
        _dialogView.CanContinue)
        {
            CurrentInteractable = null;
            CanShowContextClue = true;
            CurrentState = InteractingState.None;
            _detectInteractSignal.Invoke();
            TimelineView.Instance?.Director.playableGraph.GetRootPlayable(0).SetSpeed(1);

            _player.ChangeState(PlayerState.Idle);
            _receivedItemSprite.sprite = null;
            _currentItem.Value = null;
            
            _player.PlayerAnimations.ChangeAnim(RECEIVING_ANIMATOR_NAME, false);
            _dialogWindow.SetActive(false);
        }
    }

    public void SetCurrentInteractable(Interactable interactable)
    {
        if (interactable.PlayerInRange && CurrentInteractable == null)
            CurrentInteractable = interactable;
        
        SetCurrentState(interactable);
    }

    public void SetCurrentState(Interactable interactable)
    {
        if (CurrentState != InteractingState.Interact && (CurrentInteractable == null || CurrentInteractable == interactable))
        {
            if (interactable.PlayerInRange)
                CurrentState = InteractingState.Ready;
            else
                CurrentState = InteractingState.None;
        }
    }

    public void ResetCurrentInteractable(Interactable interactable)
    {
        if (!interactable.PlayerInRange && CurrentState == InteractingState.Ready && CurrentInteractable == interactable)
            CurrentInteractable = null;

        SetCurrentState(interactable);
        _detectInteractSignal.Invoke();
    }
}
