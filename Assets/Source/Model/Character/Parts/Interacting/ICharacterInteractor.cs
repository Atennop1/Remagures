namespace Remagures.Model.Character
{
    public interface ICharacterInteractor
    {
        InteractionState CurrentState { get; }
        void Interact();
    }
}