using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    [field: SerializeField, Header("Interactable Stuff")] public Signal Context { get; private set; }
    [field: SerializeField] protected PlayerInteracting PlayerInteract { get; private set; }
    public bool PlayerInRange { get; private set; }

    public void DetectInteracting()
    {
        if (PlayerInRange)
        {
            PlayerInteract.SetCurrentInteractable(this);
            PlayerInteract.SetCurrentState(this);
            Context.Raise();
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player) && !collision.isTrigger)
        {
            PlayerInRange = true;
            DetectInteracting();
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player) && !collision.isTrigger)
        {
            PlayerInRange = false;
            
            if (PlayerInteract.CurrentInteractable == this && PlayerInteract.CurrentState != InteractingState.Interact)
            {
                PlayerInteract.ResetCurrentState(this);
                PlayerInteract.SetCurrentState(this);
            }

            Context.Raise();
        }
    }

    public abstract void Interact();
}
