namespace Remagures.Model.Interactable
{
    public interface IInteractable
    {
        bool HasInteractionEnded { get; }
        void Interact();
        void EndInteracting();
    }
}