using Remagures.Model.Interactable;

namespace Remagures.Model.AI.NPC
{
    public interface INPCInteractable : IInteractable
    {
        bool HasInteractionStarted { get; }
    }
}