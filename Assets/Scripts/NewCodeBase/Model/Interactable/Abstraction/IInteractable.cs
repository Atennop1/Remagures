namespace Remagures.Model.Interactable
{
    public interface IInteractable
    {
        bool HasInteracted { get; }
        void Interact();
        void EndInteracting();
    }
}