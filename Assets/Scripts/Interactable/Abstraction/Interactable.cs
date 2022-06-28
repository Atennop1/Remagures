using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [field: SerializeField, Header("Interactable Stuff")] public Signal Context { get; private set; }
    [field: SerializeField] protected PlayerInteracting _playerInteract;
    public bool PlayerInRange { get; private set; }

    public void DetectInteracting()
    {
        if (PlayerInRange)
        {
            _playerInteract.SetCurrentInteractable(this);
            _playerInteract.SetCurrentState(this);
            Context.Raise();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            PlayerInRange = true;

            _playerInteract.SetCurrentInteractable(this);
            _playerInteract.SetCurrentState(this);

            Context.Raise();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            PlayerInRange = false;
            
            if (_playerInteract.CurrentInteractable == this && _playerInteract.CurrentState != InteractingState.Interact)
            {
                _playerInteract.ResetCurrentState(this);
                _playerInteract.SetCurrentState(this);
            }

            Context.Raise();
        }
    }

    public abstract void Interact();
}
