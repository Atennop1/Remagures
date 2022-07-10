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
            Context.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player) && !collision.isTrigger)
        {
            PlayerInRange = true;
            DetectInteracting();
        }

        if (CanTriggerEnter(collision))
            TriggerEnter();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerController>(out PlayerController player) && !collision.isTrigger)
        {
            PlayerInRange = false;
            
            if (PlayerInteract.CurrentInteractable == this && PlayerInteract.CurrentState != InteractingState.Interact)
            {
                PlayerInteract.ResetCurrentState(this);
                PlayerInteract.SetCurrentState(this);
            }

            Context.Invoke();
        }

        if (CanTriggerExit(collision))
            TriggerExit();
    }

    public virtual void TriggerEnter() { }
    public virtual void TriggerExit() { }

    public virtual bool CanTriggerEnter(Collider2D collision) { return true; }
    public virtual bool CanTriggerExit(Collider2D collision) { return true; }

    public abstract void Interact();
}
